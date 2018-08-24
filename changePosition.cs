using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GetComponent<Light>().enabled == true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
	}
}
