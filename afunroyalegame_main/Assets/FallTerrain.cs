using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTerrain : MonoBehaviour {

    public bool started;

    public float time;

	// Use this for initialization
    public void StartWrapper()
    {
        StartCoroutine("ReplaceRigidbody");
    }

	IEnumerator ReplaceRigidbody() {
        time = Mathf.Clamp((SyncData.worldSize * 35) - Mathf.Abs(transform.position.x / 7), 0, 36000);
        yield return new WaitForSeconds(time);
        StartCoroutine("DestroySlow");
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().freezeRotation = false;
            GetComponent<Rigidbody>().mass = 1000;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
        }
        else
        {
            
            gameObject.AddComponent<Rigidbody>();
            
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().freezeRotation = false;
            GetComponent<Rigidbody>().mass = 1000;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
        }
    }
	
	void OnCollisionEnter(Collision info)
    {
        if (GetComponent<Collider>() && info.gameObject.GetComponent<Collider>())
        {
            if (info.gameObject.tag == "Killer" && transform.childCount > 0)
            {
                if (info.gameObject.tag == "Killer" && transform.GetChild(0).gameObject.name != "Top" && transform.GetChild(0).gameObject.name != "Base")
                {
                    Destroy(gameObject);
                }
            }
            else if (info.gameObject.tag == "Killer")
            {
                Destroy(gameObject);
            }
            if (transform.GetChild(0).gameObject.name == "Top" && transform.GetChild(0).gameObject.name == "Base")
            {
                if (info.gameObject.layer == 12 || info.gameObject.layer == 16)
                {
                    Physics.IgnoreCollision(GetComponent<Collider>(), info.collider);
                }
            }
        }
    }

    IEnumerator DestroySlow()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
