using Mirror;
using UnityEngine;

public class GhostMovement : NetworkBehaviour {
    GameObject MouseFollower;
    Rigidbody rb;
    public GameObject parent;
    public float force = 200f;
	// Use this for initialization
	void Start () {
        MouseFollower = GameObject.Find("MouseFollower");
        rb = gameObject.GetComponent<Rigidbody>();
        parent.GetComponent<Health>().CmdAssignAuthority(GetComponent<NetworkIdentity>());
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (hasAuthority && rb.velocity.magnitude < 60f)
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
    }
}
