using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManagement : NetworkBehaviour {

    public GameObject AIPlayer;
    private GameObject playerSpawned;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            playerSpawned = Instantiate(AIPlayer, transform.position, transform.rotation);
            playerSpawned.GetComponent<AISetup>().parent = gameObject;
            playerSpawned.GetComponent<AISetup>().local = true;
            CmdSpawn(); //Spawn code here
        } 
	}

    [Command]
    public void CmdSpawn()
    {
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
        identity.AssignClientAuthority(GameObject.Find("LocalConnection").GetComponent<PlayerConnection>().connectionToClient);
    }
}
