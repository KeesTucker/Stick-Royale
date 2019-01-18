using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealthAI : NetworkBehaviour {
    [SyncVar]
    public float health = 200;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            if (isServer)
            {
                HealthUpdate();
            }
            Destroy(gameObject);
        }
	}

    [ClientRpc]
    public void HealthUpdate()
    {
        health = 0;
    }

    [Command]
    public void CmdUpdateHealth(float damage)
    {
        health -= damage;
    }
}
