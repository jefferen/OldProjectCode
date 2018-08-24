using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShot : MonoBehaviour
{
    public GameObject Projectile;
    public Transform firePoint;

    public bool somebool = false;

    public GameObject closestPlayerB;
    public GameObject closestMinionB;
    public GameObject closestPlayerR;
    public GameObject closestMinionR;

    // public GameObject closest;

    void Update ()
    {
        if (tag == "EnemyBlueTower")
        {
            FindMinionR();
            FindPlayerR(); //turn on/ off
        }
        if (tag == "EnemyRedTower")
        {
            FindMinionB();
            FindPlayerB(); //turn on/ off
        }
    }
    public GameObject FindMinionB()
    {
        var liste = new List<GameObject>();
        liste.AddRange(GameObject.FindGameObjectsWithTag("MinionCenterBlue"));  
        closestMinionB = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in liste)
        {
            Vector3 diff = go.transform.position - position;
            float curDistanceM = diff.sqrMagnitude;
            if (curDistanceM < distance && curDistanceM < 140 && somebool == false)
            {
                closestMinionB = go;
                distance = curDistanceM;
                Instantiate(Projectile, firePoint.position, firePoint.rotation);

                somebool = true;
                Invoke("WaitForSecondShot", 2f);
            }
        }
        return closestMinionB;
    }
    public GameObject FindMinionR()
    {
        var liste = new List<GameObject>();
        liste.AddRange(GameObject.FindGameObjectsWithTag("MinionCenterRed"));
        closestMinionR = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in liste)
        {
            Vector3 diff = go.transform.position - position;
            float curDistanceM = diff.sqrMagnitude;
            if (curDistanceM < distance && curDistanceM < 140 && somebool == false)
            {
                closestMinionR = go;
                distance = curDistanceM;
                Instantiate(Projectile, firePoint.position, firePoint.rotation);

                somebool = true;
                Invoke("WaitForSecondShot", 2f);
            }
        }
        return closestMinionR;
    }
    public GameObject FindPlayerB()
    {
        var liste = new List<GameObject>();
        liste.AddRange(GameObject.FindGameObjectsWithTag("PlayerCenterBlue"));  
        closestPlayerB = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in liste)
        {
            Vector3 diff = go.transform.position - position;
            float curDistanceM = diff.sqrMagnitude;
            if (curDistanceM < distance && curDistanceM < 140 && somebool == false)
            {
                closestPlayerB = go;
                distance = curDistanceM;
                Instantiate(Projectile, firePoint.position, firePoint.rotation);

                somebool = true;
                Invoke("WaitForSecondShot", 2f);
            }
        }
        return closestPlayerB;
    }
    public GameObject FindPlayerR()
    {
        var liste = new List<GameObject>();
        liste.AddRange(GameObject.FindGameObjectsWithTag("PlayerCenterRed"));
        closestPlayerR = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in liste)
        {
            Vector3 diff = go.transform.position - position;
            float curDistanceM = diff.sqrMagnitude;
            if (curDistanceM < distance && curDistanceM < 140 && somebool == false)
            {
                closestPlayerR = go;
                distance = curDistanceM;
                Instantiate(Projectile, firePoint.position, firePoint.rotation);

                somebool = true;
                Invoke("WaitForSecondShot", 2f);
            }
        }
        return closestPlayerR;
    }
    void WaitForSecondShot()
    {
        somebool = false;
    }
}
