using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnMe : MonoBehaviour
{
    PlayerClickToMove playClick;
    public MeleAttack MeleAttack;
    public Transform firePoint;

    public GameObject taarget;

    private float dist;
    private float distU;   

    public bool IhaveBeenTarget;

    private Vector3 t;

    private Transform target;

    private int me = 0;

    void Start()
    {
        target = GameObject.Find("Champ").GetComponent<PlayerClickToMove>().transform;
        playClick  = GameObject.Find("Champ").GetComponent<PlayerClickToMove>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            interact();
        }

        distU = Vector3.Distance(playClick.transform.position, transform.position);

        if(IhaveBeenTarget == true)
        {
            playClick.anim.SetBool("Run", true);
            var targetPosition = playClick.transform.position;
            target.transform.LookAt(targetPosition);
            targetPosition.y = target.transform.position.y;  

            t = playClick.transform.position;

            if(dist <= 17) // is this necessary? Yes it is
            {
                playClick.targetPosition = playClick.transform.position;
            }

            if(playClick.tag == "EnemyBlue")
            {
                if ((distU <= 17 && Time.time > playClick.nextFire && IhaveBeenTarget == true && tag == "EnemyRedTower") || (distU <= 17 && Time.time > playClick.nextFire&& IhaveBeenTarget == true && tag == "EnemyRed"))
                {
                    playClick.targetPosition = playClick.transform.position;
                    playClick.nextFire = Time.time + playClick.fireRate;
                    var meleAttack = Instantiate(MeleAttack, firePoint.position, firePoint.rotation);
                    meleAttack.me = taarget;
                }
            }
            if (playClick.tag == "EnemyRed")
            {
                if (distU <= 17 && Time.time > playClick.nextFire && IhaveBeenTarget == true && tag == "EnemyBlueTower" || distU <= 17 && Time.time > playClick.nextFire && IhaveBeenTarget == true && tag == "EnemyBlue")
                {
                    playClick.targetPosition = playClick.transform.position;
                    playClick.nextFire = Time.time + playClick.fireRate;
                    var meleAttack = Instantiate(MeleAttack, firePoint.position, firePoint.rotation);
                    meleAttack.me = taarget;
                }
            }
        }
    }
    void interact()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                IhaveBeenTarget = true;
            }
            else
            {
                IhaveBeenTarget = false;
            }
        }
        else
        {
            IhaveBeenTarget = false;
        }
    }
    public void OnMouseDown()
    {
        dist = Vector3.Distance(playClick.transform.position, transform.position);

        if (playClick.tag == "EnemyBlue")
        {
            playClick.targetPosition = transform.position;

            if (dist <= 17 && Time.time > playClick.nextFire && tag == "EnemyRed" || tag == "EnemyRedTower" && dist <= 17 && Time.time > playClick.nextFire) // around 15-20
            {
                var targetPosition = transform.position;
                target.transform.LookAt(targetPosition);
                targetPosition.y = target.transform.position.y;

                t = playClick.targetPosition = playClick.transform.position;
                playClick.nextFire = Time.time + playClick.fireRate;
                var meleAttack = Instantiate(MeleAttack, firePoint.position, firePoint.rotation);
                meleAttack.me = taarget;
            }
        }
        if (playClick.tag == "EnemyRed")
        {
            playClick.targetPosition = transform.position;

            if (dist <= 17 && Time.time > playClick.nextFire && tag == "EnemyBlue" || tag == "EnemyBlueTower" && dist <= 17 && Time.time > playClick.nextFire) // around 15-20
            {
                var targetPosition = transform.position;
                target.transform.LookAt(targetPosition);
                targetPosition.y = target.transform.position.y;

                t = playClick.targetPosition = playClick.transform.position;
                playClick.nextFire = Time.time + playClick.fireRate;
                var meleAttack = Instantiate(MeleAttack, firePoint.position, firePoint.rotation);
                meleAttack.me = taarget;
            }
        }
    }
}
