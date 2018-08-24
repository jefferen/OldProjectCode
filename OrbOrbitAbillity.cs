using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbOrbitAbillity : Abillity
{
    public GameObject shit;
    private TriggerableShit launcher;

    public GameObject closestEnemyB;
    public GameObject closestEnemyR;

    public GameObject prom;
    public GameObject pop;

    public float acceleration;
    public float maxSpeed;

    public float step;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<TriggerableShit>();
    }
    public override void TriggerAbillity()
    {
        launcher.OrbingOrbs();
    }

    WhatIsMyDmg what;
    public EnemyHealthManager player;
    public Collider taaaarget;

    void Awake()
    {
        player = GameObject.Find("Champ").GetComponent<EnemyHealthManager>();
        what = GameObject.Find("DmgCounter").GetComponent<WhatIsMyDmg>();
        Invoke("WakeMeUpBeforeYouGoGo", 0.8f);
        Invoke("destroy", 4f);
    }
    void WakeMeUpBeforeYouGoGo()
    {
        GetComponent<OrbOrbitAbillity>().enabled = true;
    }
    public GameObject FindMinionR()
    {
        var liste = new List<GameObject>();
        liste.AddRange(GameObject.FindGameObjectsWithTag("PlayerCenterRed"));
        liste.AddRange(GameObject.FindGameObjectsWithTag("MinionCenterRed"));
       // liste.AddRange(GameObject.FindGameObjectsWithTag("EnemyRed"));
        closestEnemyR = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in liste)
        {
            Vector3 diff = go.transform.position - position;
            float curDistanceM = diff.sqrMagnitude;
            if (curDistanceM < distance && curDistanceM < 100)
            {
                closestEnemyR = go;
                distance = curDistanceM;
            }
        }
        return closestEnemyR;
    }
    public GameObject FindMinionB()
    {
        var liste = new List<GameObject>();
        liste.AddRange(GameObject.FindGameObjectsWithTag("PlayerCenterBlue"));
        liste.AddRange(GameObject.FindGameObjectsWithTag("MinionCenterBlue"));
       // liste.AddRange(GameObject.FindGameObjectsWithTag("EnemyBlue"));
        closestEnemyB = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in liste)
        {
            Vector3 diff = go.transform.position - position;
            float curDistanceM = diff.sqrMagnitude;
            if (curDistanceM < distance && curDistanceM < 100)
            {
                closestEnemyB = go;
                distance = curDistanceM;
            }
        }
        return closestEnemyB;
    }

    void Update ()
    {
        if (what.upO1 == true)
        {
            damageToGive = player.baseDmg;
        }
        if (what.upO2 == true)
        {
            damageToGive = player.baseDmg + 10;
        }
        if (what.upO3 == true)
        {
            damageToGive = player.baseDmg + 15;
        }
        if (what.upO4 == true)
        {
            damageToGive = player.baseDmg + 20;
        }
        if (what.upO5 == true)
        {
            damageToGive = player.baseDmg + 25;
        }
        if (tag == "EnemyBlue")
        {
            FindMinionR();
            prom = closestEnemyR;
        }
        else
        {
            FindMinionB();
            pop = closestEnemyB;
        }
        if (prom != null)
        {
            CancelInvoke("destroy");
            if (prom == null)
            {
                Destroy(gameObject);
            }
            GetComponent<PerfectOrbit>().target = null;
            taaaarget = prom.GetComponentInParent<Collider>();
            step += acceleration + speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, prom.transform.position, step);
            if (step > maxSpeed)
                step = maxSpeed;
        }
        else if(pop != null)
        {
            CancelInvoke("destroy");
            if (pop == null)
            {
                Destroy(gameObject);
            }
            GetComponent<PerfectOrbit>().target = null;
            taaaarget = pop.GetComponentInParent<Collider>();
            step += acceleration + speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, pop.transform.position, step);
            if (step > maxSpeed)
                step = maxSpeed;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (tag == "EnemyBlue")
        {
            if (other.tag == "EnemyRed" && other == taaaarget)
            {
                GetComponentInChildren<Collider>().enabled = false;
                other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                other.GetComponent<EnemyHealthManager>().moneyToB = true;
                Invoke("destroy", 0.05f);
            }
        }
        if (tag == "EnemyRed")
        {
            if (other.tag == "EnemyBlue" && other == taaaarget)
            {
                GetComponentInChildren<Collider>().enabled = false;
                other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
                other.GetComponent<EnemyHealthManager>().moneyToR = true;
                Invoke("destroy", 0.05f);
            }
        }
    }
    void destroy()
    {
        Destroy(gameObject);
    }
}