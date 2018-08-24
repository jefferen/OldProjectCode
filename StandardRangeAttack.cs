using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardRangeAttack : MonoBehaviour
{

    public int damageToGive;
    public float speed;
    //public GameObject suddenEffect;
    public GameObject pro;

    public GameObject closestR;
    public GameObject closestB;

    void Awake()
    {
        if (tag == "EnemyBlue")
        {
            FindClosestEnemyR();
            pro = closestR;
        }
        if (tag == "EnemyRed")
        {
            FindClosestEnemyB();
            pro = closestB;
        }
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        if (pro == null )
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pro.transform.position, step);
        }

    }
     void OnTriggerEnter(Collider other)
     {
         if (other.name == "MinionParticle(Clone)")
         { }
         else
         {
             if (tag == "EnemyBlue")
             {
                 if (other.tag == "EnemyRed" || other.tag == "EnemyRedTower")
                 {
                    GetComponent<Collider>().enabled = false;
                    other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                    Invoke("WaitForDestructiom", 0.1f);
                }
             }
             if (tag == "EnemyRed")
             {
                 if (other.tag == "EnemyBlue" || other.tag == "EnemyBlueTower")
                 {
                    GetComponent<Collider>().enabled = false;
                    other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                    Invoke("WaitForDestructiom", 0.1f);
                }
             }
         }
     }
    GameObject FindClosestEnemyR()
    {
        var liste = new List<GameObject>();
        liste.AddRange(GameObject.FindGameObjectsWithTag("PlayerCenterRed"));
        liste.AddRange(GameObject.FindGameObjectsWithTag("EnemyRedTower"));
        liste.AddRange(GameObject.FindGameObjectsWithTag("MinionCenterRed"));
        closestR = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject goc in liste)
        {
            Vector3 diff = goc.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestR = goc;
                distance = curDistance;
            }
        }
        return closestR;
    }
    GameObject FindClosestEnemyB()
    {
        var liste = new List<GameObject>();
        liste.AddRange(GameObject.FindGameObjectsWithTag("PlayerCenterBlue"));
        liste.AddRange(GameObject.FindGameObjectsWithTag("EnemyBlueTower"));
        liste.AddRange(GameObject.FindGameObjectsWithTag("MinionCenterBlue"));
        closestB = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject goc in liste)
        {
            Vector3 diff = goc.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestB = goc;
                distance = curDistance;
            }
        }
        return closestB;
    }
    void WaitForDestructiom()
    {
        Destroy(gameObject);
    }
}