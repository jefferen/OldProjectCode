using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour
{
    public LevelManager levelManager;

	void Start ()
    {
        levelManager = FindObjectOfType<LevelManager>();
	
	}
	
	void Update ()
    {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Invoke("WaitToComeBackToLife", 2f);
       
        }
    }
    void WaitToComeBackToLife()
    {
        levelManager.RespawnPlayer();
    }
}
