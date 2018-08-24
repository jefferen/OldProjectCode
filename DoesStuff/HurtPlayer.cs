using UnityEngine;
using System.Collections;
using System;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive;
    private HealthManager hp;
    public GameObject target;
    public float rotateSpeed;
    public float speed;
    //public Transform player;
    public GameObject suddenEffect;

    void Start ()
    {
        target = GameObject.FindWithTag("PlayerCenter2");
        GetComponent<HealthManager>();
	}
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("PlayerCenter2"); // it needs this line
        if (target == null)
        {
            Debug.Log("Target is now null");
            Instantiate(suddenEffect, transform.position, Quaternion.Euler(90,0,0));
            Destroy(gameObject);
        }
        else if (target == GameObject.FindGameObjectWithTag("PlayerCenter2"))
        {
            Vector3 targetDir = target.transform.position - transform.position;
            float stepp = rotateSpeed * Time.smoothDeltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepp, 100f);
            Debug.DrawRay(transform.position, newDir, Color.red);
            transform.rotation = Quaternion.LookRotation(newDir);

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            if(target == null)
            {
                Instantiate(suddenEffect, transform.position, Quaternion.Euler(90, 0, 0));
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DmgTextController.CreateFloatingText(damageToGive.ToString(), transform);
            HealthManager.HurtPlayer(damageToGive);
            Destroy(gameObject);
        }
    }
}
