using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour {

    public Transform ragdoll;

    GameObject MouseFollower;
    Rigidbody rb;
    public float force = 600f;

    public Vector2 mouse;
    public Vector3 screenPoint;
    public Vector2 offset;
    public float angle;

    // Use this for initialization
    void Start () {
        MouseFollower = GameObject.Find("MouseFollower");
        rb = gameObject.GetComponent<Rigidbody>();
        ragdoll = GameObject.Find("Local/Ragdoll").transform;
    }
	
	void OnCollisionEnter()
    {
        ragdoll.GetComponent<SpawnRocket>().spaceDepressed = true;
    }

    void FixedUpdate()
    {
        ragdoll.position = transform.position;
        if (rb.velocity.magnitude < 120f && !ragdoll.GetComponent<SpawnRocket>().spaceDepressed) //Move toward cursor
        {
            if (MouseFollower.transform.position.x > transform.position.x)
            {
                rb.AddForce(new Vector3(force * Time.deltaTime * (MouseFollower.transform.position.x - transform.position.x), 0, 0));
            }
            else if (MouseFollower.transform.position.x < transform.position.x)
            {
                rb.AddForce(new Vector3(-force * Time.deltaTime * (transform.position.x - MouseFollower.transform.position.x), 0, 0));
            }

            if (MouseFollower.transform.position.y > transform.position.y)
            {
                rb.AddForce(new Vector3(0, force * Time.deltaTime * (MouseFollower.transform.position.y - transform.position.y), 0));
            }
            else if (MouseFollower.transform.position.y < transform.position.y)
            {
                rb.AddForce(new Vector3(0, -force * Time.deltaTime * (transform.position.y - MouseFollower.transform.position.y), 0));
            }
        }
        mouse = Input.mousePosition; //Mouse position
        screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition); 
        offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y); //Convert to world point
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg + 270f; //Convert to angle
        transform.rotation = Quaternion.Euler(0, 0, angle); //Rotate
    }
}
