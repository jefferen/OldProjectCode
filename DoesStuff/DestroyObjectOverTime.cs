using UnityEngine;
using System.Collections;

public class DestroyObjectOverTime : MonoBehaviour
{
    public float lifeTime;

	void Start ()
    {
        Destroy(gameObject, lifeTime);
    }
}
