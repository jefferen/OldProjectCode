using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodsHand : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCenter")
        {
            other.tag = "PlayerCenterRed";
        }
        if (other.tag == "Untagged")
        {          
                other.tag = "EnemyRed";
        }
    }
}
