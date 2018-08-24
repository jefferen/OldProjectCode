using UnityEngine;
using System.Collections;

public class OnlyDmgScript : MonoBehaviour
{
    public int damageToGive;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Random.Range(1, 101) == 100)
            {
                damageToGive = Random.Range(15, 20);
            }
            else if (Random.Range(1, 105) == 101)
            {
                damageToGive = 0;
            }
            else
            {
                damageToGive = Random.Range(1, 10);
            }
            DmgTextController.CreateFloatingText(damageToGive.ToString(), transform);
            HealthManager.HurtPlayer(damageToGive);
             GetComponent<HealthManager>().giveDamageAtAoe(damageToGive);
        }
    }
}
