using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPoint : MonoBehaviour
{

    public Transform ragdoll;

    GameObject MouseFollower;
    Rigidbody rb;
    public float force = 600f;

    // Use this for initialization
    void Start()
    {
        MouseFollower = GameObject.Find("MouseFollower");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        transform.position = ragdoll.position;
        Debug.Log(rb.velocity);
        transform.rotation = Quaternion.LookRotation(rb.velocity); //Rotate
    }
}
