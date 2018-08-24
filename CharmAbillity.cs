using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmAbillity : Abillity
{
    public GameObject effect;
    private TriggerableShit launcher;
    public GameObject shit;
    private bool me = false;
    public Collider taaaarget;

    WhatIsMyDmg what;

    public EnemyHealthManager player;

    void Start()
    {
        player = GameObject.Find("Champ").GetComponent<EnemyHealthManager>();
        what = GameObject.Find("DmgCounter").GetComponent<WhatIsMyDmg>();
        shit = GameObject.Find("Champ");
    }
    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<TriggerableShit>();
    }
    public override void TriggerAbillity()
    {
        launcher.Charm();
    }
    void OnTriggerEnter(Collider other)
    {
        if(tag == "EnemyBlue")
        {
            if (other.tag == "EnemyRed")
            {
                taaaarget = other;
                other.GetComponent<EnemyHealthManager>().moneyToB = true;
                if (other.name == "Minion" || other.name == "Range" || other.name == "SuperRange" ||other.name == "SuperRange")
                {
                    me = true;
                    other.GetComponent<MinonAI>().stun = true;
                    Invoke("zed", 2f);
                }
                else
                {
                    me = true;
                    other.GetComponent<PlayerClickToMove>().canMove = false;
                    Invoke("zed", 2f);
                }
                other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                Instantiate(effect, transform.position, transform.rotation);
                var child = transform.GetChild(0);
                Destroy(child.gameObject);
                transform.DetachChildren();
            }
        }
        if(tag == "EnemyRed")
        {
            if (other.tag == "EnemyBlue")
            {
                taaaarget = other;
                other.GetComponent<EnemyHealthManager>().moneyToR = true;
                if (other.name == "Minion" || other.name == "Range" || other.name == "SuperRange" || other.name == "SuperRange")
                {
                    me = true;
                    other.GetComponent<MinonAI>().stun = true;
                    if(other.GetComponent<MinonAI>().stun == false)
     
                    Invoke("zed", 2f);
                }
                else
                {
                    me = true;
                    other.GetComponent<PlayerClickToMove>().canMove = false;
                    Invoke("zed", 2f);
                }            
                other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                Instantiate(effect, transform.position, transform.rotation);
                var child = transform.GetChild(0);
                Destroy(child.gameObject);
                transform.DetachChildren();
            }
        }
    }
    void zed()
    {
        taaaarget.GetComponent<MinonAI>().stopStun();
        taaaarget.GetComponent<PlayerClickToMove>().stopStun();
    }
    void Update()
    {
        if (taaaarget == null)
        {

        }
        else if(taaaarget.GetComponent<MinonAI>().stun == false)
        {
            me = false;
        }

        if (what.upC1 == true)
        {
            damageToGive = player.baseDmg;
        }
        if (what.upC2 == true)
        {
            damageToGive = player.baseDmg + 10;
        }
        if (what.upC3 == true)
        {
            damageToGive = player.baseDmg + 20;
        }
        if (what.upC4 == true)
        {
            damageToGive = player.baseDmg + 35;
        }
        if (what.upC5 == true)
        {
            damageToGive = player.baseDmg + 50;
        }
        if (me == true)
        {
            taaaarget.transform.position = Vector3.MoveTowards(taaaarget.transform.position, shit.transform.position, 3f * Time.deltaTime);
        }
    }
}
