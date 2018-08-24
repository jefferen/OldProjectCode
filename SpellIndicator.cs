using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellIndicator : MonoBehaviour
{
    public Transform CharacterTransform;
    public bool active = false;

    public void QPressed()
    {
        Light[] lights = GetComponentsInChildren<Light>(false);
        foreach (Light light in lights)
        {
            light.cookieSize = 8;
            light.enabled = true;
        }
        active = true;
    }

    public void EPressed()
    {
        Light[] lights = GetComponentsInChildren<Light>(false);
        foreach (Light light in lights)
        {
            light.cookieSize = 11;
            light.enabled = true;
        }
        active = true;
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Light[] lights = GetComponentsInChildren<Light>(true);
            foreach (Light light in lights)
            {
                Debug.Log("Cancel Indicator");
                light.enabled = false;
                active = false;
            }
        }

        if (active == true)
        {
            var groundPlane = new Plane(Vector3.up, -CharacterTransform.position.y);
            var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDistance;

            if (groundPlane.Raycast(mouseRay, out hitDistance))
            {
                var lookAtPosition = mouseRay.GetPoint(hitDistance);
                CharacterTransform.LookAt(lookAtPosition);
            }
        }
    }
}