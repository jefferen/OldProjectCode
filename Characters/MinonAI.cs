using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinonAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public int damageToGive;
    public enum State
    {
        PATROL,
        CHASE
    }

    public State state;
    private bool alive;

    public GameObject[] waypoints;
    public int waypointInd = 0;
    public float patrolSpeed = 0.5f; // 0.5f

    public float chaseSpeed = 0.7f; // 0.7f

    Animator anim;
    EnemyHealthManager health;
    public bool dieOnce = false;

    public float dist;
    public GameObject closestEnemyB;
    public GameObject closestEnemyR;

    public GameObject Projectile;
    public Transform tailFirePoint;

    public bool iamleebool = false;
    public bool bite;

    public Collider cool;
    public Collider dmgcool;
    private Transform trans;

    public Transform changeMe;

    public bool iamthebool = false;

    public NavMeshAgent nav;
    public bool stun;

    void Start ()
    {
        nav = GetComponent<NavMeshAgent>();

        if(tag == "EnemyRed")
        {
            changeMe.tag = "MinionCenterRed";
        }
        if (tag == "EnemyBlue")
        {
            changeMe.tag = "MinionCenterBlue";
        }
        health = GetComponent<EnemyHealthManager>();
        anim = GetComponent<Animator>();
        GameObject[] minion;
        minion = GameObject.FindGameObjectsWithTag("Minion");

        agent = GetComponent<NavMeshAgent>();

        agent.updatePosition = true;

        state = MinonAI.State.PATROL;

        alive = true;
        StartCoroutine("FSM");
	}

    IEnumerator FSM()
    {
        while (alive)
        {
            switch (state)
            {
                case State.PATROL:
                    Patrol();
                    break;
                case State.CHASE:
                    Chase();
                    break;
            }
            yield return null;
        }
    }
    void Patrol()
    {
            agent.Resume();
            agent.speed = patrolSpeed;
            anim.SetBool("Walk", true);
            if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2 && dieOnce == false)
            {
                agent.SetDestination(waypoints[waypointInd].transform.position);
            }
            else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
            {
                waypointInd += 1;
                if (waypointInd >= waypoints.Length)
                {
                    waypointInd = 0;
                }
            }
            else
            {
            }       
    }
    public GameObject FindEnemyB()
    {
        var liste = new List<GameObject>();
        liste.AddRange(GameObject.FindGameObjectsWithTag("PlayerCenterBlue"));
        liste.AddRange(GameObject.FindGameObjectsWithTag("EnemyBlueTower"));
        liste.AddRange(GameObject.FindGameObjectsWithTag("MinionCenterBlue"));
        closestEnemyB = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in liste)
        {
            Vector3 diff = go.transform.position - position;
            float curDistanceM = diff.sqrMagnitude;
            if (curDistanceM < distance && curDistanceM < 90) 
            {
                closestEnemyB = go;
                distance = curDistanceM;
            }
        }
        return closestEnemyB;
    }
    public GameObject FindEnemyR()
    {
        var liste = new List<GameObject>();
        liste.AddRange(GameObject.FindGameObjectsWithTag("PlayerCenterRed"));
        liste.AddRange(GameObject.FindGameObjectsWithTag("EnemyRedTower"));
        liste.AddRange(GameObject.FindGameObjectsWithTag("MinionCenterRed"));
        closestEnemyR = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in liste)
        {
            Vector3 diff = go.transform.position - position;
            float curDistanceM = diff.sqrMagnitude;
            if (curDistanceM < distance && curDistanceM < 90)
            {
                closestEnemyR = go;
                distance = curDistanceM;
            }
        }
        return closestEnemyR;
    }
    void Chase()
    {
        // anim.SetBool("Walk", true);
        agent.speed = chaseSpeed;
        //agent.SetDestination(target.transform.position);

    }
    public void RangeAttack()
    {
        if(tag == "EnemyRed")
        {
            Projectile.tag = "EnemyRed";
            Instantiate(Projectile, tailFirePoint.position, tailFirePoint.rotation);
        }
        else
        {
            Projectile.tag = "EnemyBlue";
            Instantiate(Projectile, tailFirePoint.position, tailFirePoint.rotation);
        }
    }
    void Update ()
    {
        //col();
        agent.updateRotation = true;
        if(stun == true)
        {
        }
        else
        {
            if (tag == "EnemyRed" /*&& state != MinonAI.State.PATROL*/)
            {
                FindEnemyB();
                if (closestEnemyB == null)
                {
                    Patrol();
                }
                else
                {
                    Vector3 targetDir = closestEnemyB.transform.position - transform.position;
                    targetDir.y = 0;
                    float speed = 5f * Time.deltaTime;
                    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, speed, 0f);
                    transform.rotation = Quaternion.LookRotation(newDir);
                    dist = Vector3.Distance(closestEnemyB.transform.position, this.transform.position);
                    Vector3 direction = closestEnemyB.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    float angle = Vector3.Angle(direction, this.transform.forward);
                    if (name == "Range")
                    {
                        if (dist <= 90)
                        {
                            state = MinonAI.State.CHASE;
                            agent.SetDestination(closestEnemyB.transform.position);
                            if (dist <= 50 && iamleebool == false && angle < 60)
                            {
                                iamleebool = true;
                                agent.Stop();
                                anim.SetBool("Walk", false);
                                anim.SetBool("RangeAttack", true);
                                Invoke("stopAnim", 1f);
                                Invoke("WaitForSecondShot", 2f);
                            }
                            else if (iamleebool == false)
                            {
                                anim.SetBool("Walk", true);
                                agent.Resume();
                                agent.SetDestination(closestEnemyB.transform.position);
                            }
                        }
                    }
                    else
                    {
                        if (dist < 90)
                        {
                            state = MinonAI.State.CHASE;
                            agent.SetDestination(closestEnemyB.transform.position);
                            if (dist < 5 && bite == false && angle < 60)
                            {
                                bite = true;
                                agent.Stop();
                                anim.SetBool("Walk", false);
                                anim.SetBool("Attack", true);
                                Invoke("stopAnim", 1f);
                                Invoke("WaitForSecondShot", 2f);
                            }
                        }
                    }
                }
            }
            if (tag == "EnemyBlue")
            {
                FindEnemyR();
                if (closestEnemyR == null)
                {
                    Patrol();
                }
                else
                {
                    Vector3 targetDir = closestEnemyR.transform.position - transform.position;
                    targetDir.y = 0;
                    float speed = 5f * Time.deltaTime;
                    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, speed, 0f);
                    transform.rotation = Quaternion.LookRotation(newDir);
                    dist = Vector3.Distance(closestEnemyR.transform.position, this.transform.position);
                    Vector3 direction = closestEnemyR.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    float angle = Vector3.Angle(direction, this.transform.forward);
                    if (name == "Range")
                    {
                        if (dist <= 90)
                        {
                            state = MinonAI.State.CHASE;
                            agent.SetDestination(closestEnemyR.transform.position);
                            if (dist <= 50 && iamleebool == false && angle < 60)
                            {
                                iamleebool = true;
                                agent.Stop();
                                anim.SetBool("Walk", false);
                                anim.SetBool("RangeAttack", true);
                                Invoke("stopAnim", 1f);
                                Invoke("WaitForSecondShot", 2f);
                            }
                            else if (iamleebool == false)
                            {
                                anim.SetBool("Walk", true);
                                agent.Resume();
                                agent.SetDestination(closestEnemyR.transform.position);
                            }
                        }
                    }
                    else
                    {
                        if (dist < 90)
                        {
                            state = MinonAI.State.CHASE;
                            agent.SetDestination(closestEnemyR.transform.position);
                            if (dist < 5 && bite == false && angle < 60)
                            {
                                bite = true;
                                agent.Stop();
                                anim.SetBool("Walk", false);
                                anim.SetBool("Attack", true);
                                Invoke("stopAnim", 1f);
                                Invoke("WaitForSecondShot", 2f);
                            }
                        }
                    }
                }
            }
        } 
        if (health.enemyHealth <= 0 && dieOnce == false)
        {
            CancelInvoke("stopStun");
            stun = false;
            dieOnce = true;
            state = MinonAI.State.CHASE;
            agent.SetDestination(transform.position);
            Invoke("coooool", 0.1f);
            anim.Play("DeathHit");
            Invoke("anotherstopanim", 2.9f);
        }       
    }
    void coooool()
    {
        cool.enabled = false;
    }
    public void stopStun()
    {
        stun = false;
    }
    void anotherstopanim()
    {
        anim.StopPlayback();
        //anim.Stop();
        GetComponent<MinonAI>().enabled = false;
        if(GetComponent<MinonAI>().enabled == false)
        GetComponent<Collider>().enabled = false;
        Invoke("fall", 0.4f);
    }
    void fall()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<NavMeshAgent>().enabled = false;
    }
    void stopAnim()
    {
        anim.SetBool("RangeAttack", false);
        anim.SetBool("Attack", false);
    }
    void WaitForSecondShot()
    {
        bite = false;
        iamleebool = false;
    }
    void col()
    {
        dmgcool.enabled = true;
        Invoke("notcool", 0.3f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "MinionParticle(Clone)" || other.name == "TowerShot B(Clone)" || other.name == "TowerShot R(Clone)")
        {     }
        else
        {
            if (tag == "EnemyBlue" && name == "Minion" || name == "SuperMinion")
            {
                if (other.tag == "EnemyRed" || other.tag == "EnemyRedTower")
                {
                    dmgcool.enabled = false;                            
                    other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                }
            }
            if (tag == "EnemyRed" && name == "Minion" || name == "SuperMinion")
            {
                if (other.tag == "EnemyBlue" || other.tag == "EnemyBlueTower")
                {
                    dmgcool.enabled = false;
                    other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                }
            }
        }
    }
    void notcool()
    {
        dmgcool.enabled = false;
    }
}
