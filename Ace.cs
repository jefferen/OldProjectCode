using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ace : MonoBehaviour
{

	void Update ()
    {
		if(GetComponentInParent<Rigidbody>() == null)
        {
            Invoke("destroy", 0.25f);
        }
	}
    void destroy()
    {
        Destroy(gameObject);
    }
}
