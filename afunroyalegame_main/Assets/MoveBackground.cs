using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {

    public Transform cam;
	
	// Update is called once per frame
	void Update () {
        if (cam)
        {
            transform.position = new Vector3(cam.position.x - cam.position.x / 6, cam.position.y - cam.position.y / 50, 100);
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
