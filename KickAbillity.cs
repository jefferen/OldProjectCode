using UnityEngine;
using System.Collections;
using System;

public class KickAbillity : Abillity
{
    public int gunDamage;
    public float weponRange;
    public Player player;
    private TriggerableShit launcher;

    void Start()
    {
        shootRateBall = 5f;
        player = FindObjectOfType<Player>();
    }

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<TriggerableShit>();
    }
    public override void TriggerAbillity()
    {
        launcher.kick();
    }
    public GameObject hitEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponentInParent<EnemyHealthManager>().giveDamage(damageToGive);
            Instantiate(hitEffect, other.transform.position, other.transform.rotation);
        }
        Destroy(gameObject);
    }
}
