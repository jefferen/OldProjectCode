using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public int damageToGive = 50;
    public GameObject target;

    void Start()
    {

        if(tag == "EnemyRed")
        {
           target = GameObject.FindGameObjectWithTag("PlayerCenterBlue");
        }
        if (tag == "EnemyBlue")
        {
           target = GameObject.FindGameObjectWithTag("PlayerCenterRed");
        }
    }

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 20f * Time.deltaTime);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(tag == "EnemyRed" && other.tag == "EnemyBlue")
        {
            other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
            Invoke("Destroy", 0.05f);
        }
        if (tag == "EnemyBlue" && other.tag == "EnemyRed")
        {
            other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
            Invoke("Destroy", 0.05f);
        }
    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
