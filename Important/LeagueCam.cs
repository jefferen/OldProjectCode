using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeagueCam : MonoBehaviour
{
    [System.Serializable]
    public class PositioningSettings
    {
        public float panSmooth = 7;
        public bool invertPan = true;
        public float distanceFromGround = 40;
        public bool allowZoom = true;
        public float zoomSmooth = 5;
        public float zoomStep = 5;
        public float maxZoom = 25;
        public float minZoom = 80;
        [HideInInspector]
        public float newDistance = -40;

    }
    [System.Serializable]
    public class OrbitSettings
    {
        public float xRotation = 50;
        public float yRotation = 0;
        public bool allowYOrbit = true;
        public float yOrbitSmooth = 0.5f;
    }
    [System.Serializable]
    public class InputSettings
    {
        public string PAN = "MousePan";
        public string ORBIT_Y = "MouseTurn";
        public string ZOOM = "Mouse ScrollWheel";
    }

    public PositioningSettings position = new PositioningSettings();
    public OrbitSettings orbit = new OrbitSettings();
    public InputSettings input = new InputSettings();

    public LayerMask groundLayer;
    //public Transform target;
    Vector3 destination = Vector3.zero;
    Vector3 camVelocity = Vector3.zero;
    Vector3 currentMousePosition = Vector3.zero;
    Vector3 previousMousePosition = Vector3.zero;
    float orbitInput, panInput, zoomInput;
    int panDirection = 0;

    void Start ()
    {
        panInput = 0;
        orbitInput = 0;
        zoomInput = 0;
	}

    void GetInput()
    {
        panInput = Input.GetAxis(input.PAN);
        orbitInput = Input.GetAxis(input.ORBIT_Y);
        zoomInput = Input.GetAxis(input.ZOOM);

        previousMousePosition = currentMousePosition;
        currentMousePosition = Input.mousePosition;
    }
	
	void Update ()
    {
        GetInput();

        if (position.allowZoom)
            Zoom();
 
        if (orbit.allowYOrbit)
            Rotate();
        PanWorld();
	}

    void FixedUpdate()
    {
        HandleCameraDistance();
    }
    void PanWorld()
    {
        Vector3 targetPos = transform.position;

        if (position.invertPan)
            panDirection = -1;
        else
            panDirection = 1;

        if(panInput > 0)
        {
            targetPos += transform.right * (currentMousePosition.x - previousMousePosition.x) *
                position.panSmooth * panDirection * Time.deltaTime;
            targetPos += Vector3.Cross(transform.right, Vector3.up) * (currentMousePosition.y - previousMousePosition.y) *
                position.panSmooth * panDirection * Time.deltaTime;
        }
        transform.position = targetPos;
    }
    void HandleCameraDistance()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 100, groundLayer))
        {
            destination = Vector3.Normalize(transform.position - hit.point) * position.distanceFromGround;
            destination += hit.point;

            transform.position = Vector3.SmoothDamp(transform.position, destination, ref camVelocity, 0.03f);
        }
    }
    void Zoom()
    {
        position.newDistance += position.zoomStep * -zoomInput;

        position.distanceFromGround = Mathf.Lerp(position.distanceFromGround, position.newDistance, position.zoomSmooth * Time.deltaTime);

        if(position.distanceFromGround < position.maxZoom)
        {
            position.distanceFromGround = position.maxZoom;
            position.newDistance = position.maxZoom;
        }
        if (position.distanceFromGround < position.minZoom)
        {
            position.distanceFromGround = position.minZoom;
            position.newDistance = position.minZoom;
        }
    }
    void Rotate()
    {
        if(orbitInput > 0)
        {
            orbit.yRotation += (currentMousePosition.x - previousMousePosition.x) * orbit.yOrbitSmooth * Time.deltaTime;
        }
        transform.rotation = Quaternion.Euler(orbit.xRotation, orbit.yRotation, 0);
    }
}
