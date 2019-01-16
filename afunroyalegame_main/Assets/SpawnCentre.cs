using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnCentre : NetworkBehaviour {

    public GameObject centre;

	// Use this for initialization
	void Start () {
        if (isServer)
        {
            GameObject cent = Instantiate(centre, transform.position + new Vector3(0, 0, 0.1f), Quaternion.identity);
            cent.transform.parent = transform;
            NetworkServer.Spawn(cent);
        }
	}
}
