using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [Tooltip("do some god danm dmg human")]
    public int damageToGive;
    [Tooltip("I like my speed with fries")]
    public float speed;
    //public GameObject suddenEffect;

    #region   

    public GameObject prom;
    public GameObject pop;

    public GameObject closestR;
    public GameObject closestB;

    public float acceleration;
    public float maxSpeed;

    public float step;
    public Collider target;

    #endregion

    void Awake()
    {
        if(tag == "EnemyBlue")
        {
            FindClosestTowerBlue();
            prom = closestB.GetComponent<TowerShot>().closestMinionR;
            pop = closestB.GetComponent<TowerShot>().closestPlayerR;
        }
        if (tag == "EnemyRed")
        {
            FindClosestTowerRed();
           prom = closestR.GetComponent<TowerShot>().closestMinionB;
           pop = closestR.GetComponent<TowerShot>().closestPlayerB;
        }
    }
    void Update()
    {
        step += acceleration + speed * Time.deltaTime;
        if (prom == null && pop == null)
        {
            Destroy(gameObject);
        }
        else if(prom != null)
        {
            target = prom.GetComponentInParent<Collider>();
            transform.position = Vector3.MoveTowards(transform.position, prom.transform.position, step);
            if (step > maxSpeed)
                step = maxSpeed;
        }
        else
        {
            target = pop.GetComponentInParent<Collider>();
            transform.position = Vector3.MoveTowards(transform.position, pop.transform.position, step);
            if (step > maxSpeed)
                step = maxSpeed;
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if(tag == "EnemyBlue")
        {
            if (other.tag == "EnemyRed" && other == target)
            {
                GetComponent<Collider>().enabled = false;
                other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                Invoke("WaitForDestructiom", 0.05f);
            }
        }
        if (tag == "EnemyRed")
        {
            if (other.tag == "EnemyBlue" && other == target)
            {
                GetComponent<Collider>().enabled = false;
                other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                Invoke("WaitForDestructiom", 0.05f);
            }
        }
    }
    GameObject FindClosestTowerRed()
    {
        GameObject[] tower;
        tower = GameObject.FindGameObjectsWithTag("EnemyRedTower");
        closestR = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject goc in tower)
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
    GameObject FindClosestTowerBlue()
    {
        GameObject[] tower;
        tower = GameObject.FindGameObjectsWithTag("EnemyBlueTower");
        closestB = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject goc in tower)
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
