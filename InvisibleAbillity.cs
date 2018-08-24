using UnityEngine;
using System.Collections;
using System;

public class InvisibleAbillity : Abillity
{
    public Player player;
    private TriggerableShit launcher;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<TriggerableShit>();
    }
    public override void TriggerAbillity()
    {
        launcher.invisible();
    }
}
