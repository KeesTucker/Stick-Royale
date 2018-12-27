using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAffector : MonoBehaviour {

    public int abilityIndex;
    public float startForce;
    public Rigidbody rb;
    public bool onServer;
    public GameObject localRelay;

    IEnumerator Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        for (int i = 0; i < 200; i++)
        {
            rb.AddForce(startForce * Time.deltaTime * transform.up);
        }
        if (!onServer)
        {
            localRelay = GameObject.Find("Local/Physics Animator");
        }
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    IEnumerator OnCollisionEnter(Collision collisionInfo)
    {
        if (!onServer)
        {
            if (collisionInfo.transform.parent.gameObject.name == "Local" || collisionInfo.transform.parent.parent.gameObject.name == "Local")
            {
                if (abilityIndex == 0)
                {
                    localRelay.GetComponent<PlayerMovement>().maxSpeed = 20;
                    onServer = true;
                    yield return new WaitForSeconds(5f);
                    localRelay.GetComponent<PlayerMovement>().maxSpeed = 45;
                    Destroy(gameObject);
                }
            }
        }
    }
}
