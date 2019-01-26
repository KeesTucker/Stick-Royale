using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowAI : MonoBehaviour {

    public Transform parent;

    public Transform pos;

    public Transform aim;

    public Vector3 offset;

    public Vector3 target;

    public Rigidbody rb;

    public float force;
	
	// Update is called once per frame
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, offset.z);
    }

	void FixedUpdate () {
        if (parent && !pos)
        {
            pos = parent;
        }
        if (pos && aim)
        {
            target = (((pos.position * 5) + aim.position) / 6) + offset;
        }
        else if (pos)
        {
            target = pos.position;
        }
        rb.AddForce(new Vector3(force * Time.deltaTime * (target.x - transform.position.x), force * Time.deltaTime * (target.y - transform.position.y), 0));
    }
}
