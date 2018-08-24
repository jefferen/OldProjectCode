using UnityEngine;
using System.Collections;
using System;

public class MagicBallAbillity : Abillity
{
    public GameObject shit;
    private TriggerableShit launcher;
    public bool me;
    public bool me2;

    public float acceleration;
    public float accelerationBack;
    public float maxSpeed;
    public float speeeed;
    public float speeeedBack;
    public float step;
    public float stepBack;

    WhatIsMyDmg what;

    EnemyHealthManager player;

    public void Start()
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
        launcher.fire();          
    }
    void OnTriggerEnter(Collider other)
    {
        if(tag == "EnemyBlue")
        {
            if (other.tag == "EnemyRed")
            {
                other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                other.GetComponent<EnemyHealthManager>().moneyToB = true;
            }
        }
        if(tag == "EnemyRed")
        {
            if (other.tag == "EnemyBlue")
            {
                other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                other.GetComponent<EnemyHealthManager>().moneyToR = true;
            }
        }
        if(other.name == "Champ" && me == true) 
        {
            Invoke("destroy", 0.05f);
        }
    }
    void Update()
    {
        if (what.upB1 == true)
        {
            damageToGive = player.baseDmg;
        }
        if (what.upB2 == true)
        {
            damageToGive = player.baseDmg + 10;
        }
        if (what.upB3 == true)
        {
            damageToGive = player.baseDmg + 20;
        }
        if (what.upB4 == true)
        {
            damageToGive = player.baseDmg + 40;
        }
        if (what.upB5 == true)
        {
            damageToGive = player.baseDmg + 70;
        }

        if (me == false && me2 == false)
        {
            step -= acceleration + speeeed * Time.deltaTime;
        }
        else if(me2 == true)
        {
            step = 0f;
        }
        if(me == true)
        {
            stepBack += accelerationBack + speeeedBack * Time.deltaTime;
        }
        // IT WOOOORKSSSS!!!!!!! NOW IT JUST NEED TO IGNORE THE Y-PLANE! FUCK!!!
        Invoke("WaitASec", 0.5f);
        if(me == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, shit.transform.position, stepBack);
            transform.position = new Vector3(transform.position.x, 4.249166f, transform.position.z);
        }
        else
        {
            transform.Translate(Vector3.forward * 1.5f * step);
        }
        if (stepBack > maxSpeed)
            stepBack = maxSpeed;
    }
    void destroy()
    {
        var child = transform.GetChild(0);
        var child2 = transform.GetChild(1);
        var child3 = transform.GetChild(2);
        var child4 = transform.GetChild(3);
        var child5 = transform.GetChild(4);
        var child6 = transform.GetChild(5);
        var child7 = transform.GetChild(6);
        var child8 = transform.GetChild(7);
        var child9 = transform.GetChild(8);
        var child10 = transform.GetChild(9);
        var child11 = transform.GetChild(10);
        var child12 = transform.GetChild(11);

        Destroy(child.gameObject);
        Destroy(child2.gameObject);
        Destroy(child3.gameObject);
        Destroy(child4.gameObject);
        Destroy(child5.gameObject);
        Destroy(child6.gameObject);
        Destroy(child7.gameObject);
        Destroy(child8.gameObject);
        Destroy(child9.gameObject);
        Destroy(child10.gameObject);
        Destroy(child11.gameObject);
        Destroy(child12.gameObject);

        transform.DetachChildren();
        Destroy(gameObject);
    }
    void WaitASec()
    {
        me2 = true;
        Invoke("tuuurnBack", 0.05f);
    }
    void tuuurnBack()
    {
        me = true;
    }
}