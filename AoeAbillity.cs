using UnityEngine;
using System.Collections;
using System;

public class AoeAbillity : Abillity
{
    private TriggerableShit launcher;
    public Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<TriggerableShit>();
    }
    public override void TriggerAbillity()
    {
        /*if (GameObject.Find("LeCircle").GetComponent<Light>().enabled == false)
        {
            launcher.aoeIndicatorOn();
        }*/
         if (GameObject.Find("LeCircle").GetComponent<Light>().enabled == true)
        {
            launcher.AoeCircle();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Aoe is called to enemy");
            other.GetComponentInParent<EnemyHealthManager>().giveDamageAtAoe(damageToGive);           
        }
    }
}
