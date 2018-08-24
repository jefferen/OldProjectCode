using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBoxActivatet : MonoBehaviour
{
    GameObject me;

	void Start ()
    {
       me = GameObject.Find("Chat");
        me.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // what is the enter key called?? it is called return!
        {
            Debug.Log("Im pressed");
            me.SetActive(true);
        }
        if(me.activeInHierarchy == true)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                me.SetActive(false);
            }
        }
	}
}