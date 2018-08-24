using UnityEngine;
using System.Collections;
using System;

public class RunAbillity : Abillity
{
    public Player player;
    private TriggerableShit launcher;

    void Start()
    {
        shootRateBall = 5f;
        player = FindObjectOfType<Player>();
    }

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<TriggerableShit>();
    }
    public override void TriggerAbillity()
    {
        launcher.run();
    }
}
