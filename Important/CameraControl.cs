using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Animator anim;

    [System.Serializable]
    public class PositionSettings
    {
        public Vector3 targetPosOffset = new Vector3(0, 6f, -8f);
        public float lookSmooth = 0.09f;       
        public float distanceFromTarget;
        public float zoomSmooth = 10;
        public float maxZoom = -2;
        public float minZoom = 15;
        public bool smoothFollow = true;
        public float smooth = 0.05f;

        [HideInInspector]
        public float newDistance = -2;
        [HideInInspector]
        public float adjustmentDistance = -1;
    }

    [System.Serializable]
    public class OrbitSettings
    {
        public float zRotation;
        public float xRotation;
        public float yRotation = 20;
        public float maxXRotation;
        public float minXRotation;
        public float vOrbitSmooth;
        public float hOrbitSmooth;
    }

    [System.Serializable]
    public class InputSettings
    {
        public string ORBIT_HORIZONTAL_SNAP = "OrbitHorizontalSnap";
        public string ORBIT_HORIZONTAL = "OrbitHorizontal";
        public string ORBIT_VERTICAL = "OrbitVertical";
        public string ZOOM = "Mouse ScrollWheel";
    }

    [System.Serializable]
    public class DebugSettings
    {
        public bool drawDesiredCollisionLines = true;
        public bool drawAdjustedCollisionLines = true;
    }

    public PositionSettings position = new PositionSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();
    public DebugSettings debug = new DebugSettings();
    public CollisionHandler collision = new CollisionHandler();

    Vector3 targetPos = Vector3.zero;
    Vector3 destination = Vector3.zero;
    Vector3 adjustedDestination = Vector3.zero;
    Vector3 camVel = Vector3.zero;
    float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput;
    Vector3 previousMousePos = Vector3.zero;
    Vector3 currentMousePos = Vector3.zero;

    private Player launcher;
    public float rotateSpeed;

    private Vector3 targetPoint;
    private Quaternion camRotation;

    private Player player;

    public void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<Player>();

    }
    void Start ()
    {
       /* targetPos = target.position + position.targetPosOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
        destination += targetPos;*/
        player = FindObjectOfType<Player>();
        SetCameraTarget(target);
        anim = GetComponent<Animator>();

        MoveToTarget();

        collision.Initialize(Camera.main);
        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);
        previousMousePos = currentMousePos = Input.mousePosition;
    }
    void GetInput()
    {
        vOrbitInput = Input.GetAxisRaw(input.ORBIT_VERTICAL);
        hOrbitInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL);
        hOrbitSnapInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL_SNAP);
        zoomInput = Input.GetAxisRaw(input.ZOOM);
    }
	void LateUpdate ()
    {
        GetInput();
        ZoomInOnTarget();
	
	}
    void SetCameraTarget(Transform t)
    {
        target = t;

        if(target != null)
        {
           if (target.GetComponent<Player>())
            {
                player = target.GetComponent<Player>();
            }
        }
    }
    void FixedUpdate()
    {
        MoveToTarget();
        LookAtTarget();
        OrbitTarget();

        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desiredCameraClipPoints);
        collision.CheckColliding(targetPos);
        position.adjustmentDistance = collision.GetAdjustedDistanceWithRayFrom(targetPos);
    }
    void MoveToTarget()
    {
        if (Input.GetKey(KeyCode.Mouse0) ^ Input.GetKey(KeyCode.Mouse1))
        {            
            targetPos = target.position + position.targetPosOffset;
            destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
            destination += targetPos;
            //targetPos = position.targetPosOffset;
             //destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0) * -Vector3.forward * position.distanceFromTarget;
             //destination += targetPos;*/
            orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
            orbit.yRotation += +hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;
        }
        else {
            targetPos = target.position + position.targetPosOffset;
        destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * position.distanceFromTarget;
        destination += targetPos;
    }
        if (collision.colliding)
        {
            adjustedDestination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * -Vector3.forward * position.adjustmentDistance;
            adjustedDestination += targetPos;

            if (position.smoothFollow)
            {
                transform.position = Vector3.SmoothDamp(transform.position, adjustedDestination, ref camVel, position.smooth);
            }
            else
                transform.position = adjustedDestination;              
        }
        else
        {
            if (position.smoothFollow)
            {
                transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVel, position.smooth);
            }
            else
                transform.position = adjustedDestination;
        }   
    }
    void LookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,100 * Time.deltaTime);
    }
    void OrbitTarget()
    {
        if (hOrbitSnapInput > 0)
        {
            orbit.yRotation = 0;
        }

        if (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.Mouse0))
        {
            player.anim.Play("Walk 0");
           // anim.SetBool("Walk 0", true);    
            orbit.yRotation = 0;
            player.forwardInput =  1;       
            orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
            orbit.yRotation += +hOrbitInput/3 * orbit.hOrbitSmooth * Time.deltaTime;
            player.transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            player.turnInput = orbit.yRotation;
        }
        /*else
        {
            anim.SetBool("Walk 0", false);
        }*/
        if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1) && orbit.yRotation != 0 && player.forwardInput == 1)
        {
           orbit.yRotation = 0;
        }

        if (orbit.xRotation > orbit.maxXRotation)
        {
            orbit.xRotation = orbit.maxXRotation;
        }
        if(orbit.xRotation < orbit.minXRotation)
        {
            orbit.xRotation = orbit.minXRotation;
        }       
    }
    void ZoomInOnTarget()
    {
        position.distanceFromTarget -= zoomInput * position.zoomSmooth * Time.deltaTime;

        if(position.distanceFromTarget > position.maxZoom)
        {
            position.distanceFromTarget = position.maxZoom;
        }
        if(position.distanceFromTarget < position.minZoom)
        {
            position.distanceFromTarget = position.minZoom;
        }
    }
    void StopFocus()
    {
        orbit.yRotation = 0;
    }
    [System.Serializable]
    public class CollisionHandler
    {
        public LayerMask collisionLayer;
        [HideInInspector]
        public bool colliding = false;
        [HideInInspector]
        public Vector3[] adjustedCameraClipPoints;
        [HideInInspector]
        public Vector3[] desiredCameraClipPoints;

        Camera camera;

        public void Initialize(Camera cam)
        {
            camera = cam;
            adjustedCameraClipPoints = new Vector3[5];
            desiredCameraClipPoints = new Vector3[5];
        }
        public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray)
        {
            if (!camera)
                return;
            intoArray = new Vector3[5];

            float z = camera.nearClipPlane;
            float x = Mathf.Tan(camera.fieldOfView / 3.41f) * z;
            float y = x / camera.aspect;

            intoArray[0] = (atRotation * new Vector3(-x, y, z)) + cameraPosition;

            intoArray[1] = (atRotation * new Vector3(x, y, z)) + cameraPosition;

            intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + cameraPosition;

            intoArray[3] = (atRotation * new Vector3(x, -y, z)) + cameraPosition;

            intoArray[4] = cameraPosition - camera.transform.forward;
        }

        bool CollisionDetectedAtClipPoints(Vector3[] clipPoints, Vector3 fromPosition)
        {
            for(int i = 0; i < clipPoints.Length; i++)
            {
                Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
                float distance = Vector3.Distance(clipPoints[i], fromPosition);
                if(Physics.Raycast(ray, distance, collisionLayer))
                {
                    return true;
                }
            }
            return false;
        }
        public float GetAdjustedDistanceWithRayFrom(Vector3 from)
        {
            float distance = -1;
            for(int i = 0; i < desiredCameraClipPoints.Length; i++)
            {
                Ray ray = new Ray(from, desiredCameraClipPoints[i] - from);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    if (distance == -1)
                        distance = hit.distance;
                    else
                    {
                        if (hit.distance < distance)
                            distance = hit.distance;
                    }
                }
            }
            if (distance == -1)
                return 0;
            else
                return distance;
        }
        public void CheckColliding(Vector3 targetPosition)
        {
            if(CollisionDetectedAtClipPoints(desiredCameraClipPoints, targetPosition))
            {
                colliding = true;
            }
            else
            {
                colliding = false;
            }
        }
    }
}
