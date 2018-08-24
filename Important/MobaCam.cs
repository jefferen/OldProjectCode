using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobaCam : MonoBehaviour
{
    public float scrollSpeed;
    public float moveScrollPower;

    public float topBarrier;
    public float botBarrier;
    public float leftBarrier;
    public float rightBarrier;

    [System.Serializable]
    public class PositioningSettings
    {
        public float distanceFromTarget = -50;
        public bool allowZoom = true;
        public float zoomSmooth = 100;
        public float zoomStep = 2;
        public float maxZoom = -30;
        public float minZoom = -60;
        public bool smoothFollow = true;
        public float smooth = 0.05f;
        [HideInInspector]
        public float newDistance = -50;

    }
    [System.Serializable]
    public class InputSettings
    {
        public string ZOOM = "Mouse ScrollWheel";
    }

    public PositioningSettings position = new PositioningSettings();
    public InputSettings input = new InputSettings();

    public Transform target;
    Vector3 destination = Vector3.zero;
    Vector3 camVelocity = Vector3.zero;
    Vector3 currentMousePosition = Vector3.zero;
    Vector3 previousMousePosition = Vector3.zero;
    float zoomInput;

    Animator anim;

    Vector3 varvar;

    void Start ()
    {
        anim = GetComponent<Animator>();

        SetCameraTarget(target);

        if (target)
        {
          //  MoveToTarget();
        }
	}
	
    public void SetCameraTarget(Transform t)
    {
        target = t;

        if(target == null)
        {
            Debug.Log("Your camera needs a target");
        }
    }

    void GetInput()
    {
        zoomInput = Input.GetAxisRaw(input.ZOOM);
    }

	void Update ()
    {
        transform.position = transform.position;
        GetInput();
       
            ZoomInOnTarget();       

		if(Input.mousePosition.y >= Screen.height * topBarrier)
        {
            if (Input.mousePosition.y >= Screen.height * topBarrier +50)
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveScrollPower, Space.World);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * scrollSpeed, Space.World);
            }
        }
        if (Input.mousePosition.y <= Screen.height * botBarrier)
        {
            if(Input.mousePosition.y <= Screen.height * botBarrier - 50)
            {
                transform.Translate(Vector3.left * Time.deltaTime * moveScrollPower, Space.World);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed, Space.World);
            }

        }
        if (Input.mousePosition.x >= Screen.height * rightBarrier)
        {
            if(Input.mousePosition.x >= Screen.height * rightBarrier + 50)
            {
                transform.Translate(Vector3.back * Time.deltaTime * moveScrollPower, Space.World);
            }
            else
            {
                transform.Translate(Vector3.back * Time.deltaTime * scrollSpeed, Space.World);
            }
        }
        if (Input.mousePosition.x <= Screen.height * leftBarrier)
        {
            if (Input.mousePosition.x <= Screen.height * leftBarrier - 50)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * moveScrollPower, Space.World);
            }
            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime * scrollSpeed, Space.World);
            }
        }
    }

    void LateUpdate()
    {
        varvar = transform.position;
        varvar.x = Mathf.Clamp(varvar.x, -78, 55);
        varvar.z = Mathf.Clamp(varvar.z, -65, 65);
        transform.position = varvar;
    }

    void FixedUpdate()
    {
        if (target && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("pressing button calling movetotarget");
            MoveToTarget();
        }
    }
    void MoveToTarget()
    {
        Debug.Log("calling movetotarget");
        // Focus target target target
       // destination = target.position;
        transform.position = new Vector3(target.position.x - 19.189f, 29f, target.position.z + 3f);


           // transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVelocity, position.smooth);
        
    }
    void ZoomInOnTarget()
    {
// Zoom Zoom Zoom
    }
}
