using UnityEngine;
using System.Collections;

public class MinionController : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    Transform tag_Player;
    public float turnSpeed, moveSpeed;
    
    public Rigidbody rBody;

    private float inputH;
    private float inputV;
    float turnInput;
    public float forwardInput;

    Quaternion targetRotation;
    public Quaternion TargetRotation;

    public float waitBetweenShots;
    private float shotCounter;
    public Transform firePoint;
    public GameObject magicAttack;
    public float speed;
    public Player player;
    public GameObject play;
    public GameObject minion;
    public bool keepInRange;
    bool LastRange;
    bool righto;
    bool lefto;
    public bool call;
    public bool left;
    public bool followingAlly;
    public bool patrolling = true;

    public GameObject target;

    public class InputSettigns
    {
        public string FORWARD_AXIS = "Vertical";
        public string TURN_AXIS = "Horizontal";
    }
    public InputSettigns inputSetting = new InputSettigns();
    Vector3 velocity = Vector3.zero;
    float lockposx = 0;
    public GameObject[] waypoints;
    private int waypointInd = 0;

    void Start ()
    {
        patrolling = true;
        followingAlly = false;
        waypoints = GameObject.FindGameObjectsWithTag("Waypoints");
        waypointInd = Random.Range(0, waypoints.Length);
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        righto = false;
        lefto = false;
        call = false;
        LastRange = false;
        keepInRange = false;
        shotCounter = waitBetweenShots;
        player = FindObjectOfType<Player>();
        minion = GameObject.FindWithTag("Minion");
        play = GameObject.FindWithTag("Player");
        tag_Player = GameObject.FindGameObjectWithTag("Player").transform;
        //tag_Player = GameObject.FindGameObjectWithTag("EnemyBlue").transform;
        //tag_Player = GameObject.FindGameObjectWithTag("EnemyRed").transform;
        targetRotation = transform.rotation;
        rBody = GetComponent<Rigidbody>();     
    }
    IEnumerator LateCall()
    {
        yield return new WaitForSecondsRealtime(1);
        if (shotCounter < 0)
        {
            GameObject go = (GameObject)Instantiate(magicAttack, firePoint.position, firePoint.rotation);
           // go.GetComponent<Rigidbody>().velocity = firePoint * speed;
            shotCounter = waitBetweenShots;
        }
    }
    void Patrol()
    {
        patrolling = true;
        if(Vector3.Distance (this.transform.position, waypoints[waypointInd].transform.position) >= 2)
        {
            agent.SetDestination(waypoints[waypointInd].transform.position);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        else if (Vector3.Distance (this.transform.position, waypoints[waypointInd].transform.position) <= 2) 
        {
            waypointInd = Random.Range(0,waypoints.Length);       
        }
        else
        {
            transform.Translate(Vector3.zero);
        }   
            Vector3 direction = play.transform.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);
            float dist = Vector3.Distance(play.transform.position, this.transform.position);
            if (dist <= 15)
            {
                patrolling = false;          
        }
    }
    void OnTriggerEnter (Collider other)
    {
        if(gameObject.tag == "Minion")
        {
            Debug.Log("how many times is this called?");
            Vector3 direction = play.transform.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);
            float dist = Vector3.Distance(play.transform.position, this.transform.position);
            if (dist <= 15 && followingAlly == false)
            {
                Debug.Log("Follwoing enemy ally");
                followingAlly = true;
                patrolling = false;       
            }
        }
    }
    void Update ()
    {
        Vector3 direction = play.transform.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        float dist = Vector3.Distance(play.transform.position, this.transform.position);
        if(patrolling == true)
        {
            Patrol();
        }        
        if(patrolling == false)
        {
            Debug.Log("Going after player");
            transform.rotation = Quaternion.Euler(lockposx, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            if (lefto == true)
            {
                Debug.Log("Going left");
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(tag_Player.position - transform.position),
                turnSpeed * Time.deltaTime);
                transform.position += -transform.right * moveSpeed * Time.deltaTime;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                Invoke("NoLeftRight", 5f);
            }
            if (righto == true)
            {
                Debug.Log("Going right");
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(tag_Player.position - transform.position),
                turnSpeed * Time.deltaTime);
                transform.position += transform.right * moveSpeed * Time.deltaTime;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                Invoke("NoLeftRight", 5f);
            }
            if (dist < 10 && angle < 30)
            {
                Debug.Log("Shot");
                StartCoroutine(LateCall());
            }
            if (dist <= 2 && keepInRange == false && call == false) // this is only called for one frame
            {
                Debug.Log("Distance <= 2");
                keepInRange = true;
                call = true;
                CallIsTrue();
            }
            if (dist >= 2 && dist <= 10 && (keepInRange == false) && angle < 30)
            {
                Debug.Log("Distance is 2-10");
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(tag_Player.position - transform.position),
                turnSpeed * Time.deltaTime);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
            if (dist >= 10 && dist <= 15 && followingAlly == true)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(tag_Player.position - transform.position),
                turnSpeed * Time.deltaTime);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
            if (dist >= 2 && dist <= 10 && (keepInRange == true) && call == false)  // check this line too!!!
            {
                keepInRange = false; // this is an experiment
                Debug.Log("Distance is 2-10 + range");
                call = true;
                CallIsTrue();
            }
            if (dist <= 15 && dist >= 10 && (keepInRange == true)) // this is only called for one frame     Check this code! the movement will not countinue
            {
                keepInRange = false;
                LastRange = true;
                Debug.Log("Distance is 10-15 + range");
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(tag_Player.position - transform.position),
                turnSpeed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                transform.position += transform.forward * moveSpeed * Time.deltaTime; // moves for one frame
            }
            if (dist <= 2 && LastRange == true)
            {
                LastRange = false;
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(tag_Player.position - transform.position),
                turnSpeed * Time.deltaTime);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
            if (dist >= 2 && LastRange == true)
            {
                LastRange = false;
                transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(tag_Player.position - transform.position),
                turnSpeed * Time.deltaTime);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
            if (dist >= 15 && keepInRange == true && followingAlly == true || dist >= 15 && LastRange == true && followingAlly == true)
            {
                followingAlly = false;
                patrolling = true;
                Debug.Log("Distance is more then 15 + range");
                keepInRange = false;
                LastRange = false;
                transform.Translate(Vector3.zero);
            }
        }     
        shotCounter -= Time.deltaTime;
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }
    void CallIsTrue()
    {
        var randomNumber = Random.Range(0, 3);
        if (randomNumber > 1)
            righto = true;
        else
            lefto = true;
    }
    void NoLeftRight()
    {
        call = false;
        righto = false;
        lefto = false;
    }
}
