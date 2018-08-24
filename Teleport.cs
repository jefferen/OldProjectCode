using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject targetB;
    public GameObject targetR;

    public GameObject recall;

    PlayerClickToMove move;

    private Vector3 t;
    private bool b;

    GameObject go;

    void Start()
    {
        move = GetComponent<PlayerClickToMove>();
    }

    void Update ()
    {
        if (Input.GetKey(KeyCode.B) && b == false)
        {
            b = true;
            t = transform.position;
            go = Instantiate(recall, transform.position, transform.rotation);
            Invoke("Teeleport", 5f);
        }
        else if (move.transform.position != t && b == true)
        {
            b = false;
            CancelInvoke("Teeleport");
            Destroy(go);
        }
	}
    void Teeleport()
    {
        Invoke("tp", 0.1f);
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
            r.enabled = false;
        if (tag == "EnemyRed")
        {
            transform.position = targetR.transform.position;
            GetComponent<PlayerClickToMove>().targetPosition = targetR.transform.position;
        }
        else
        {
            transform.position = targetB.transform.position;
            GetComponent<PlayerClickToMove>().targetPosition = targetB.transform.position;
        }
    }
    void tp()
    {
        Renderer[] rs = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
            r.enabled = true;
        if (tag == "EnemyRed")
        {
            transform.position = targetR.transform.position;
        }
        else
        {
            transform.position = targetB.transform.position;
        }
    }
}
