using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDisclaimer : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (Input.GetKey("space"))
        {
            gameObject.SetActive(false);
        }
	}
}
