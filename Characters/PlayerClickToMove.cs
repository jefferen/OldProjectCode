using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PlayerClickToMove : MonoBehaviour
{
    public GameObject clickEffect;
    public NavMeshAgent playerAgent;
    RaycastHit hit;
    Ray ray;
    public Vector3 targetPosition;
    public float n = 0.777f;

    public Vector3 velocity = Vector3.zero;

    public HealthManager healthManager;

    public Animator anim;
    private Rigidbody rBody;

    public float speed;
    public float speedo;
    public Transform player;
    private EnergyManager energy;

    public LevelManager levelManager;
    public bool canMove;

    public float fireRate = 1; // this is ok! might be able to do better
    public float nextFire = 1;

    void Start ()
    {
        targetPosition = transform.position;

        playerAgent = GetComponent<NavMeshAgent>();
        canMove = true;
        healthManager = FindObjectOfType<HealthManager>();
        levelManager = FindObjectOfType<LevelManager>();
        rBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
	
	void Update ()
    {
        if (canMove == false)
        {
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                GetInterAction();
                anim.SetBool("Run", true);
            }
            if (transform.position == targetPosition)
            {
                anim.SetBool("Run", false);
            }
        }
            movePlayer();
    }
    void GetInterAction()
    {       
             Plane plane = new Plane(Vector3.up, transform.position); // test
             Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit interactionInfo;
             float point = 0f; // test
                               //var targetPosition = interactionRay.GetPoint(hitdist);
            if (plane.Raycast(interactionRay,out point)) // test
             {
                 Instantiate(clickEffect, interactionRay.GetPoint(point), Quaternion.identity);
                 targetPosition = interactionRay.GetPoint(point);
                // anim.SetBool("Run", true);
             }


            /* if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
             {
                // Transform targetPosition = interactionRay.GetPoint(RaycastHit);
                 //Instantiate(clickEffect, transform.position, transform.rotation);
                 GameObject interactedObject = interactionInfo.collider.gameObject;
                     if (tag == "EnemyRed")
                 {
                     if (interactedObject.tag == "EnemyBlue")
                     {
                         interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
                         if (interactedObject.tag == "EnemyRed")
                         {

                         }
                     }
                     else
                     {
                         playerAgent.destination = interactionInfo.point;
                     }
                 }
                 if (tag == "EnemyBlue")
                 {
                     if (interactedObject.tag == "EnemyRed")
                     {
                         interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
                         if (interactedObject.tag == "EnemyBlue")
                         {

                         }
                     }
                     else
                     {
                         playerAgent.destination = interactionInfo.point;
                     }
                 }
             }*/
        }
    public void stopStun()
    {
        canMove = true;
    }
    public void movePlayer()
    {
        playerAgent.SetDestination(targetPosition);
    }
    public void StopMoving()
    {
    }
    void FixedUpdate()
    {
        rBody.velocity = transform.TransformDirection(velocity);
    }
}
