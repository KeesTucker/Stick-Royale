using UnityEngine;
using Mirror;
using System.Collections;
using System.Collections.Generic;

public class GhostMovement : NetworkBehaviour {
    GameObject MouseFollower;
    Rigidbody rb;
    public GameObject proj;
    public GameObject parent;
    public float force = 200f;
    public int shots = 3;
    public int abilityIndex = 0;

    [SyncVar]
    public bool spaceDepressed;

    public bool spaceDepressedLocal = true;

    public bool shootable = true;

    // Use this for initialization
    void Start () {
        MouseFollower = GameObject.Find("MouseFollower");
        rb = gameObject.GetComponent<Rigidbody>();
        parent.GetComponent<Health>().CmdAssignAuthority(GetComponent<NetworkIdentity>());
    }
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && hasAuthority && shots >= 0)
        {
            CmdSetSpace(true);
            shots--;
        }
        if (spaceDepressed && spaceDepressedLocal)
        {
            spaceDepressedLocal = false;
            GameObject projInst = Instantiate(proj, transform.position, Quaternion.identity);
            projInst.GetComponent<AbilityAffector>().abilityIndex = abilityIndex;
            projInst.GetComponent<AbilityAffector>().onServer = hasAuthority; //none of this crap works and ienumerators arnt called for some reasons
            DelayFalse();
        }
    }

    IEnumerator DelayFalse()
    {

        yield return new WaitForSeconds(0.5f);
        CmdSetSpace(false);
    }

    [Command]
    void CmdSetSpace(bool state)
    {
        spaceDepressed = state;
        if (state)
        {
            RpcSetSpace();
            spaceDepressedLocal = true;
        }
    }

    [ClientRpc]
    void RpcSetSpace()
    {
        spaceDepressedLocal = true;
    }

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
