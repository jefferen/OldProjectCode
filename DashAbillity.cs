using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbillity : Abillity {

    public GameObject shit;
    private TriggerableShit launcher;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<TriggerableShit>();
    }
    public override void TriggerAbillity()
    {
        launcher.Dash(); // dash first cooldown should be 120
    }
}
