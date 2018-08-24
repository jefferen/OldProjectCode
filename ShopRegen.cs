using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRegen : MonoBehaviour
{
    public GameObject Laser;
    public Transform LaserPoint;
    public bool somebool = false;

    void OnTriggerStay(Collider other)
    {
        if(other.name == "ChampEnemy" && tag == "EnemyRed" && other.tag == "EnemyRed")
        {
            other.GetComponent<EnemyHealthManager>().regen = 800;
        }
        if (other.name == "Champ" && tag == "EnemyBlue" && other.tag == "EnemyBlue")
        {
            other.GetComponent<EnemyHealthManager>().regen = 800;
        }
        if (tag == "EnemyRed" && other.tag == "EnemyBlue" && somebool == false)
        {
            somebool = true;
            Instantiate(Laser, LaserPoint.position, LaserPoint.rotation);
            Invoke("WaitForSecondShot", 0.1f);
            Laser.tag = "EnemyRed";
        }
        if (tag == "EnemyBlue" && other.tag == "EnemyRed" && somebool == false)
        {
            somebool = true;
            Instantiate(Laser, LaserPoint.position, LaserPoint.rotation);
            Invoke("WaitForSecondShot", 0.1f);
            Laser.tag = "EnemyBlue";
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.name == "ChampEnemy" && tag == "EnemyRed" && other.tag == "EnemyRed")
        {
            other.GetComponent<EnemyHealthManager>().regen = other.GetComponent<EnemyHealthManager>().baseRegen;
        }
        if (other.name == "Champ" && tag == "EnemyBlue" && other.tag == "EnemyBlue")
        {
            other.GetComponent<EnemyHealthManager>().regen = other.GetComponent<EnemyHealthManager>().baseRegen;
        }
    }
    void WaitForSecondShot()
    {
        somebool = false;
    }
}
