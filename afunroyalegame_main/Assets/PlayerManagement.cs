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
            CmdSpawn(); //Spawn code here
        } 
	}

    [Command]
    public void CmdSpawn()
    {
        playerSpawned = Instantiate(AIPlayer, transform.position, transform.rotation);
        playerSpawned.GetComponent<AISetup>().parent = gameObject;
        NetworkServer.SpawnWithClientAuthority(playerSpawned, connectionToClient);
    }
}
