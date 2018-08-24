using UnityEngine;
using System.Collections;

public class ExhaustPlayer : MonoBehaviour
{
    public int exhaustToGive;

    void Start()
    {

    }

    void Update()
    {

    }
    void exhaust()
    {    
            EnergyManager.exhaustPlayer(exhaustToGive);
        
    }
}
