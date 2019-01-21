using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {

    public Transform aim;

    public GameObject cam;

	// Use this for initialization
	void Start () {
        if (GetComponent<AISetup>().local)
        {
            GameObject camera = Instantiate(cam, transform.position, Quaternion.identity);
            camera.GetComponent<CamFollowAI>().parent = transform;
            camera.GetComponent<CamFollowAI>().aim = aim;
            aim.gameObject.GetComponent<followMouse>().cam = camera.GetComponent<Camera>();
        }
    }
}
