using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CamControl : NetworkBehaviour {

    public Transform aim;

    public GameObject cam;

    // Use this for initialization
    public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            GameObject camera = Instantiate(cam, transform.position, Quaternion.identity);
            camera.GetComponent<CamFollowAI>().parent = transform;
            camera.GetComponent<CamFollowAI>().aim = aim;
            aim.gameObject.GetComponent<followMouse>().cam = camera.GetComponent<Camera>();
        }
    }
}
