using UnityEngine;
using System.Collections;

public class OnlyDmgAtAoe : MonoBehaviour
{
    public int damageToGive;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("doing dmg");
            DmgTextController.CreateFloatingText(damageToGive.ToString(), transform);
            // HealthManager.HurtPlayer(damageToGive);
            HealthManager.dodmg = true;
           // GetComponent<Player>().GetComponent<HealthManager>().giveDamageAtAoe(damageToGive);
          // GetComponent<HealthManager>().giveDamageAtAoe(damageToGive);
        }
    }
}
