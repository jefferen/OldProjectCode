using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private NavMeshAgent playerAgent;

    public virtual void MoveToInteraction(NavMeshAgent playerAgent)
    {
        this.playerAgent = playerAgent;
        playerAgent.destination = this.transform.position;

        Interact();
    }
    public virtual void Interact()
    {

    }
	void Start ()
    {
		
	}
	

	void Update ()
    {
		
	}
}
