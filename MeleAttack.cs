using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttack : MonoBehaviour
{
    public GameObject me;
    public int baseDmg;
    public int damageToGive;

    public Collider target;
    public EnemyHealthManager player;

    void Start()
    {
        player = GameObject.Find("Champ").GetComponent<EnemyHealthManager>();
        target = me.GetComponentInParent<Collider>();
    }

    void Update ()
    {
        damageToGive = player.baseDmg + baseDmg;

        float step = 25f * Time.deltaTime;
        if(target == null)
        {
            Invoke("destroy", 0.02f);
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, me.transform.position, step);
    }
    void OnTriggerEnter(Collider other)
    {
        if (tag == "EnemyBlue")
        {
            if ((other.tag == "EnemyRed" || other.tag == "EnemyRedTower") && other == target)
            {
                other.GetComponent<EnemyHealthManager>().moneyToB = true;
                
                other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                Invoke("destroy", 0.05f);
            }
        }
        if (tag == "EnemyRed")
        {
            if ((other.tag == "EnemyBlue" || other.tag == "EnemyBlueTower") && other == target)
            {
                other.GetComponent<EnemyHealthManager>().moneyToR = true;

                other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                Invoke("destroy", 0.05f);
            }
        }
    }
    void destroy()
    {
        Destroy(gameObject);
    }
}
