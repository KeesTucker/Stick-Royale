using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISetup : MonoBehaviour {

    public Collider[] colliders;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int v = 0; v < colliders.Length; v++)
            {
                Physics.IgnoreCollision(colliders[i], colliders[v]);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
