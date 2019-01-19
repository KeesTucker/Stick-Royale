using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AISetup : NetworkBehaviour {

    public Collider[] colliders;

    public GameObject relay;

    public GameObject relaySpawned;

    public GameObject ragdoll;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int v = 0; v < colliders.Length; v++)
            {
                Physics.IgnoreCollision(colliders[i], colliders[v]);
            }
        }
        
        relaySpawned = Instantiate(relay, ragdoll.transform.position, transform.rotation);
        relaySpawned.GetComponent<PlayerSetupAI>().parent = gameObject;
        NetworkServer.Spawn(relaySpawned);
        CmdAssignAuthority(relaySpawned.GetComponent<NetworkIdentity>());
	}

    [Command]
    public void CmdAssignAuthority(NetworkIdentity identity)
    {
        NetworkConnection currentOwner = identity.clientAuthorityOwner;
        if (currentOwner == connectionToClient)
        {
            return;
        }
        else
        {
            if (currentOwner != null)
            {
                identity.RemoveClientAuthority(currentOwner);
            }
            identity.AssignClientAuthority(connectionToClient);
        }
    }
}
