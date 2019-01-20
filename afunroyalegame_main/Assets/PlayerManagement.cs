using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManagement : NetworkBehaviour {

    public GameObject playerControlledPlayer;
    public GameObject AIPlayer;
    private GameObject playerSpawned;

	// Use this for initialization
	void Start () {
        if (isServer)
        {
            CmdSpawn(transform.position, AIPlayer); //Spawn code here
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [Command]
    public void CmdSpawn(Vector3 pos, GameObject spawnable)
    {
        playerSpawned = Instantiate(spawnable, pos, transform.rotation);
        playerSpawned.GetComponent<AISetup>().parent = gameObject;
        playerSpawned.GetComponent<AISetup>().local = true;
        NetworkServer.Spawn(playerSpawned);
    }

    [Command]
    public void CmdAssignAuthority(NetworkIdentity identity)
    {
        NetworkConnection currentOwner = identity.clientAuthorityOwner;
        if (currentOwner != null)
        {
            identity.RemoveClientAuthority(currentOwner);
        }
        identity.AssignClientAuthority(GameObject.Find("LocalRelay").GetComponent<PlayerSetup>().connectionToClient); //Need to make a placeholder player object for this, but do that once everything else is sorted
    }
}
