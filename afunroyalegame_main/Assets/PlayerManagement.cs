using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManagement : NetworkBehaviour {

    public GameObject AIPlayer;
    public GameObject bot;
    public GameObject playerSpawned;
    public GameObject playerSpawnedReal;
    public int numPlayers = 1;
    public int currentNum = 1;
    public int totalPlayers;
    private Vector3 pos;
    public PlayerManagement playerManagement;

	// Use this for initialization
	void Start () {
        numPlayers = SyncData.numPlayers;
        if (!isLocalPlayer && isServer)
        {
            playerManagement = GameObject.Find("LocalConnection").GetComponent<PlayerManagement>();
        }
        if (isLocalPlayer)
        {
            CmdSpawn(); //Spawn code here
            if (isServer)
            {
                for (int i = 0; i < numPlayers; i++)
                {
                    CmdBotSpawn();
                }
            }
        }
	}

    [Command]
    public void CmdSpawn()
    {
        if (playerManagement)
        {
            if (playerManagement.currentNum % 2 == 0)
            {
                pos = new Vector3((playerManagement.currentNum / 2) * 500, 0, 0);
            }
            else
            {
                pos = new Vector3((int)(-playerManagement.currentNum / 2) * 500, 0, 0);
            }
        }
        playerSpawned = Instantiate(AIPlayer, pos, transform.rotation);
        playerSpawned.GetComponent<AISetup>().parent = gameObject;
        NetworkServer.SpawnWithClientAuthority(playerSpawned, connectionToClient);
        if (playerManagement)
        {
            playerManagement.currentNum++;
        }
        else
        {
            currentNum++;
        }
    }

    [Command]
    public void CmdBotSpawn()
    {
        if (currentNum % 2 == 0)
        {
            pos = new Vector3((currentNum / 2) * 500, 0, 0);
        }
        else
        {
            pos = new Vector3((int)(-currentNum / 2) * 500, 0, 0);
        }
        playerSpawned = Instantiate(bot, pos, transform.rotation);
        playerSpawned.GetComponent<AISetup>().parent = gameObject;
        NetworkServer.SpawnWithClientAuthority(playerSpawned, connectionToClient);
        currentNum++;
    }
}
