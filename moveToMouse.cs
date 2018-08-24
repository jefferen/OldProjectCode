using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToMouse : MonoBehaviour
{
    public float step;

    void Update()
    {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f; 
        if (plane.Raycast(interactionRay, out point))
        {
            transform.position = Vector3.MoveTowards(transform.position, interactionRay.GetPoint(point), step * Time.deltaTime);
        }
    }
}