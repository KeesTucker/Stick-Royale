using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMove : MonoBehaviour {

    public Transform ragdoll;

    Transform MouseFollower;
    Rigidbody rb;
    public float force = 600f;
    public float rotForce = 10f;

    public Vector2 mouse;
    public Vector3 screenPoint;
    public Vector2 offset;
    public float angle;

    public SpawnRocketAI spawnRocket;

    // Use this for initialization
    IEnumerator Start () {
        yield return new WaitForSeconds(0.2f);
        MouseFollower = ragdoll.Find("AIAim");
        rb = gameObject.GetComponent<Rigidbody>();
        spawnRocket = ragdoll.gameObject.GetComponent<SpawnRocketAI>();
    }
	
	void OnCollisionEnter(Collision info)
    {
        if (spawnRocket && info.gameObject.layer != 13)
        {
            spawnRocket.spaceDepressed = true;
        }
    }

    void FixedUpdate()
    {
        if (spawnRocket)
        {
            if (ragdoll && !spawnRocket.spaceDepressed)
            {
                if (ragdoll.GetComponent<SpawnRocketAI>().hasAuthority)
                {
                    ragdoll.position = transform.position;
                }
                else
                {
                    transform.position = ragdoll.position;
                }
            }
            if (rb.velocity.magnitude < 120f && !spawnRocket.spaceDepressed) //Move toward cursor
            {
                if (MouseFollower.position.x > transform.position.x)
                {
                    rb.AddForce(new Vector3(force * Time.deltaTime * (MouseFollower.position.x - transform.position.x), 0, 0));
                }
                else if (MouseFollower.position.x < transform.position.x)
                {
                    rb.AddForce(new Vector3(-force * Time.deltaTime * (transform.position.x - MouseFollower.position.x), 0, 0));
                }

                if (MouseFollower.position.y > transform.position.y)
                {
                    rb.AddForce(new Vector3(0, force * Time.deltaTime * (MouseFollower.position.y - transform.position.y), 0));
                }
                else if (MouseFollower.position.y < transform.position.y)
                {
                    rb.AddForce(new Vector3(0, -force * Time.deltaTime * (transform.position.y - MouseFollower.position.y), 0));
                }
            }

            if (rb.velocity.y < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, -1 * Mathf.Atan(rb.velocity.x / rb.velocity.y) * Mathf.Rad2Deg + 180);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, -1 * Mathf.Atan(rb.velocity.x / rb.velocity.y) * Mathf.Rad2Deg);
            }
        }
    }
}
