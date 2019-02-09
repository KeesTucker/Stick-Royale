using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTerrain : MonoBehaviour {

    public bool started;

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(Mathf.Clamp(300 - Mathf.Abs(transform.position.x / 10), 0, 3600));
        StartCoroutine("DestroySlow");
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().freezeRotation = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
        }
        else
        {
            
            gameObject.AddComponent<Rigidbody>();
            
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().freezeRotation = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
        }
    }
	
	void OnCollisionEnter(Collision info)
    {
        if (GetComponent<Collider>() && info.gameObject.GetComponent<Collider>())
        {
            if (info.gameObject.tag == "Kill" && transform.GetChild(0).gameObject.name != "Top" && transform.GetChild(0).gameObject.name != "Base")
            {
                Destroy(gameObject);
            }
            if (info.gameObject.layer == 12 || info.gameObject.layer == 16)
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), info.collider);
            }
        }
    }

    IEnumerator DestroySlow()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
