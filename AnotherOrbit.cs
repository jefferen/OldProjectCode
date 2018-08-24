using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherOrbit : MonoBehaviour {

  public float RotationSpeed = 100f;
    public float OrbitSpeed = 50f;
    public float DesiredMoonDistance;
    public GameObject target;

    void Start ()
    {
        target = GameObject.Find("Champ");
        DesiredMoonDistance = Vector3.Distance(target.transform.position, transform.position);
    }

    void LateUpdate ()
    {
        if (target == null)
        {
           enabled = false;

        }
        else
        {
            transform.position = new Vector3(transform.position.x, 3.2f, transform.position.z);
            transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
            transform.RotateAround(target.transform.position, Vector3.up, OrbitSpeed * Time.deltaTime);

            //fix possible changes in distance
            float currentMoonDistance = Vector3.Distance(target.transform.position, transform.position);
            Vector3 towardsTarget = transform.position - target.transform.position;
            transform.position += (DesiredMoonDistance - currentMoonDistance) * towardsTarget.normalized;
        }
    }
}
