using UnityEngine;
using System.Collections;

public class AoeFollowMouse : MonoBehaviour
{
	void LateUpdate ()
    {
        if(GetComponentInChildren<Light>().enabled == true)
        {
            transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z + 90);
        }
    }
}