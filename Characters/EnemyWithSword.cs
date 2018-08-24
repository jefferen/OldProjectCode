using UnityEngine;
using System.Collections;

public class EnemyWithSword : MonoBehaviour
{
    Animator anim;
    public GameObject play;
    Transform tag_Player;
    public float turnSpeed, moveSpeed;
    public bool attacking;
    Vector3 velocity = Vector3.zero;
    public float timeLeft = 5;
    public float maxtime = 5;
    float lockposx = 0;

    public GameObject groundEffect;
    public Transform groundhitSpot;
    public GameObject Meteor;
    public Transform MeteorFirePoint;

    public bool somebool;
    bool OnlyOne;

    void Start ()
    {
        OnlyOne = false;
        somebool = false;
        anim = GetComponentInChildren<Animator>();
       // PatrolState();
        play = GameObject.FindWithTag("Player");
        tag_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(lockposx, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
       
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            timeLeft = maxtime;
        }
 
        ChaseState();
        AttackState();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == ("unitychan"))
        {
            Debug.Log("Found player");
            ChaseState();
        }
    }
    void AttackState()
    {
        moveSpeed = 1.2f;        
        Vector3 direction = play.transform.position - new Vector3(transform.position.x, 2.27f, transform.position.z);

        float angle = Vector3.Angle(direction, this.transform.forward);
        float dist = Vector3.Distance(play.transform.position, this.transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(tag_Player.position - transform.position),
        turnSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (GetComponentInChildren<EnemyHealthManager>().maxEnemyHealth - GetComponentInChildren<EnemyHealthManager>().enemyHealth
                 >= 0.5 * GetComponentInChildren<EnemyHealthManager>().maxEnemyHealth && OnlyOne == false)
        {
            moveSpeed = 1.5f;
            OnlyOne = true;
            Invoke("once", 20f);
            somebool = true;
            Invoke("HereComesTheMeteor", 2f);
        }
        
        if (dist >= 3.5)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        if (dist <= 3.5 && angle < 30 && attacking == false && timeLeft < 1 && somebool == false) // && rest timer is = 0)
        {
            transform.Translate(Vector3.zero);
            Debug.Log("Picking attack");
           // attacking = true;
            var randomNumber = Random.Range(0, 5);
            if (randomNumber == 1)
            {
                attacking = true;
                anim.SetBool("MeleAttack", true);
                Invoke("StopAttack", 0.8f);
            }
            if (randomNumber == 2)
            {
                attacking = true;
                anim.SetBool("OverHandAttack", true);
                Invoke("Effect", 0.7f);

                Invoke("StopAttack", 0.8f);
            }
            if (randomNumber == 3)
            {
                attacking = true;
                anim.SetBool("Twister", true);
                Invoke("StopAttack", 3.8f);
            }
            if (randomNumber == 4)
            {
                attacking = true;
                anim.SetBool("Hurricane", true);
                Invoke("StopAttack", 0.8f);
            }
        }
    }
    void once()
    {
        OnlyOne = false;
    }
    void HereComesTheMeteor()
    {
        Instantiate(Meteor, MeteorFirePoint.position, Quaternion.Euler(90, 0, 0));
        Invoke("MeteorAttackOver", 4f);
    }
    void MeteorAttackOver()
    {
        somebool = false;
    }
    void Effect()
    {
        Instantiate(groundEffect, groundhitSpot.position, groundhitSpot.rotation);
    }
    void StopAttack()
    {
        attacking = false;
        anim.SetBool("Hurricane", false);
        anim.SetBool("Twister", false);
        anim.SetBool("OverHandAttack", false);
        anim.SetBool("MeleAttack", false);
    }
    void PatrolState()
    {
        moveSpeed = 0.5f;
    }
    void DieState()
    {

    }
    void ChaseState()
    {
        moveSpeed = 1.2f;

        Vector3 direction = play.transform.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        float dist = Vector3.Distance(play.transform.position, this.transform.position);

        if(dist >= 10 && dist <= 15)
        {
           // targetRotation *= Quaternion.AngleAxis(moveSetting.rotateVel * turnInput * Time.deltaTime, Vector3.up);
            //transform.rotation = targetRotation;

            //transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation);

            transform.rotation = Quaternion.Slerp(transform.rotation,
         Quaternion.LookRotation(tag_Player.position - transform.position),
        turnSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        if (dist <= 10 && angle < 30)
        {
            Debug.Log("going to attack mode");
            AttackState();
        }
    }
}
