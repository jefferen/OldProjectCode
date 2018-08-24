using UnityEngine;
using System.Collections;

public class MeteorAbillity : MonoBehaviour
{
    public GameObject Explosion;
    public Transform PointOfExplosion;

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
    void OnTriggerEnter(Collider other)
    {
        Instantiate(Explosion, PointOfExplosion.position, PointOfExplosion.rotation);
        Destroy(gameObject);
    }
}
