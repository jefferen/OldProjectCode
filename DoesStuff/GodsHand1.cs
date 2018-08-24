using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodsHand1 : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCenter")
        {
            other.tag = "PlayerCenterBlue";
        }
        if (other.tag == "Untagged")
        {
            other.tag = "EnemyBlue";
        }
    }
}
