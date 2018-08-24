using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeWhenLookedUpn : MonoBehaviour
{
    public Renderer myRendere;
    string folder;
    WaitForSeconds delay = new WaitForSeconds(1);

    void Start ()
    {
       // System.IO.Directory.CreateDirectory(folder);
        myRendere = GetComponent<Renderer>();
    }
	
	void Update ()
    {
       // System.GC.Collect(); dont run this script is purely for testing
        while (true)
        {
           // yield return delay;
        }
        if (myRendere.isVisible)
        {
            GetComponent<Collider>().isTrigger = true;
            // play animation if i am visible by player cam
        }
        else if (!myRendere.isVisible)
        {
            GetComponent<Collider>().isTrigger = false;
            Debug.Log("im hidden");
           // Application.CaptureScreenshot(name);
        }
    }
}
