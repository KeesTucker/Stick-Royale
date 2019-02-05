using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManagement : NetworkBehaviour {

    public GameObject AIPlayer;
    public GameObject bot;
    private GameObject playerSpawned;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            CmdSpawn(); //Spawn code here
            if (isServer)
            {
                CmdBotSpawn();
            }
        } 
	}

    [Command]
    public void CmdSpawn()
    {
        playerSpawned = Instantiate(AIPlayer, transform.position, transform.rotation);
        playerSpawned.GetComponent<AISetup>().parent = gameObject;
        NetworkServer.SpawnWithClientAuthority(playerSpawned, connectionToClient);
    }

    [Command]
    public void CmdBotSpawn()
    {
        playerSpawned = Instantiate(bot, transform.position, transform.rotation);
        playerSpawned.GetComponent<AISetup>().parent = gameObject;
        NetworkServer.SpawnWithClientAuthority(playerSpawned, connectionToClient);
    }
}
