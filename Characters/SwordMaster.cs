using UnityEngine;
using System.Collections;

public class SwordMaster : MonoBehaviour
{
    Animator anim;
    public float turnSpeed, moveSpeed;
    public GameObject play;
    Transform tag_Player;
    Vector3 velocity = Vector3.zero;
    public float timeLeft = 5;
    public float maxtime = 5;
    public bool attacking;

    bool somebool;
    bool OnlyOne;
    bool charge;
    bool avoidAttack;
    int number;
    bool stopAvoid;
    bool anotherOne;
    bool dodging;

    void Start()
    {
        dodging = false;
        anotherOne = false;
        stopAvoid = false;
        avoidAttack = false;
        charge = false;
        somebool = false;
        OnlyOne = false;
        attacking = false;
        anim = GetComponent<Animator>();
        play = GameObject.FindWithTag("Player");
        tag_Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha3) && avoidAttack == false)
        {
            avoidAttack = true;
             number = Random.Range(0, 2);
        }
        if(avoidAttack == true && stopAvoid == false)
        {
            if (number == 1)
            {
                dodging = true;
                transform.Translate(Vector3.right * 2.8f * moveSpeed * Time.smoothDeltaTime);
                Invoke("StopAvoid", 0.3f);
            }
            else
            {
                dodging = true;
                transform.Translate(Vector3.left * 2.8f * moveSpeed * Time.smoothDeltaTime);
                Invoke("StopAvoid", 0.3f);
            }
        }
        if (charge == true)
        {
            transform.Translate(Vector3.forward * moveSpeed * 3 * Time.smoothDeltaTime);
        }
        if (moveSpeed == 3)
        {
            Debug.Log("movespeed became 3");
        }
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            timeLeft = maxtime;
        }
        ChaseState();
        AttackState();
    }
    void StopAvoid()
    {
        stopAvoid = true;
        dodging = false;
        var ind = Random.Range(0, 4); // being called more then once
        if (ind == 1 && anotherOne == false)
        {
            anotherOne = true;
            Invoke("AvoidAttack", 5f);
        }
        if (ind == 2 && anotherOne == false)
        {
            anotherOne = true;
            Invoke("AvoidAttack", 10f);
        }
        if (ind == 3 && anotherOne == false)
        {
            anotherOne = true;
            Invoke("AvoidAttack", 15f);
        }
    }
    void AvoidAttack()
    {
        anotherOne = false;
        avoidAttack = false;
        stopAvoid = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == ("unitychan"))
        {
            Debug.Log("Found player");
            ChaseState();
        }
    }
    void PatrolState()
    {
        moveSpeed = 1f;
    }
    void AttackState()
    {
        Debug.Log("AttackState");
        moveSpeed = 1.2f;
        Vector3 direction = play.transform.position - new Vector3(transform.position.x, 1.3f, transform.position.z);

        float angle = Vector3.Angle(direction, this.transform.forward);
        float dist = Vector3.Distance(play.transform.position, this.transform.position);

        if(charge == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(tag_Player.position - transform.position),
            turnSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        if (GetComponentInChildren<EnemyHealthManager>().maxEnemyHealth - GetComponentInChildren<EnemyHealthManager>().enemyHealth
            >= 0.25 * GetComponentInChildren<EnemyHealthManager>().maxEnemyHealth && OnlyOne == false)
        {
            moveSpeed = 1.5f;
            if (dist >= 2.4 && dodging == false)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            if (dist <= 2.4 && angle < 30 && attacking == false && timeLeft < 1 && somebool == false) // && rest timer is = 0)
            {
                transform.Translate(Vector3.zero);
                anim.SetBool("neverendingCombo", true);
                attacking = true;
                Invoke("StopAttack", 1f);
            }
        }
        if (dist >= 2.4 && dodging == false)
        {
            Debug.Log("Trying to move");
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        if (dist <= 2.4 && angle < 30 && attacking == false && timeLeft < 1 && somebool == false && anim.GetBool("Charge") == false && anim.GetBool("Combo") == false) // && rest timer is = 0)
        {
            transform.Translate(Vector3.zero);
            anim.SetBool("Mele", true);
            attacking = true;
            Invoke("StopAttack", 1f);
            Invoke("RandomAttack", Random.Range(3, 7));
        }
    }
    void RandomAttack()
    {
        Debug.Log("Random is called");
        somebool = true;
        var randomNumber = Random.Range(0, 2);
        if (randomNumber == 1)
        {
            Invoke("chargingUp", 1.7f);
            attacking = true;
            anim.SetBool("Charge", true);
            Invoke("StopAttack", 3.50f);
        }
        else
        {
            attacking = true;
            anim.SetBool("Combo", true);
            Invoke("StopAttack", 2.3f);
        }
    }
    void chargingUp()
    {
        charge = true;
    }
    void ChaseState()
    {
        moveSpeed = 1.2f;
        Vector3 direction = play.transform.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        float dist = Vector3.Distance(play.transform.position, this.transform.position);

        if (dist >= 10 && dist <= 15)
        {
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
    void StopAttack()
    {
        charge = false;
        somebool = false;
        attacking = false;
        anim.SetBool("Mele", false);
        anim.SetBool("Charge", false);
        anim.SetBool("Combo", false);
    }
}