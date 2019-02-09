using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AISetup : NetworkBehaviour
{

    public Collider[] colliders;

    public GameObject parent;

    public bool local = false;

    public NetworkManager manager;

    public HealthAI health;

    public PlayerManagement playerManagement;

    public bool dead = false;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("_NetworkManager").GetComponent<NetworkManager>();
        playerManagement = GameObject.Find("LocalConnection").GetComponent<PlayerManagement>();
        playerManagement.totalPlayers++;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!isServer && !GetComponent<PlayerControl>())
            {
                colliders[i].gameObject.layer = 14;
            }
            for (int v = 0; v < colliders.Length; v++)
            {
                Physics.IgnoreCollision(colliders[i], colliders[v]);
            }
        }
    }

    public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            manager = GameObject.Find("_NetworkManager").GetComponent<NetworkManager>();
            if (GameObject.Find("Player(Clone)").GetComponent<SpawnRocketAI>().ready)
            {
                SyncData.failed = true;
                manager.StopClient();
            }
            else
            {
                SyncData.failed = false;
            }
            local = true;
            if (GetComponent<PlayerControl>())
            {
                foreach (ChunkLoad chunk in GameObject.Find("Terrain").GetComponentsInChildren<ChunkLoad>())
                {
                    chunk.local = transform;
                }
                GameObject.Find("Inventory").GetComponent<UpdateUI>().refrenceKeeper = GetComponent<RefrenceKeeperAI>();
            }
        }
    }

    void Update()
    {
        if (playerManagement.totalPlayers <= 1)
        {
            GetComponent<RefrenceKeeperAI>().updateUI.won.SetActive(true);
            if (Input.GetKey("f") && GetComponent<PlayerControl>() && hasAuthority)
            {
                if (!isServer)
                {
                    manager.StopClient();
                }
                else
                {
                    manager.StopClient();
                    manager.StopServer();
                }
            }
        }
        if (health.health <= 0)
        {
            if (!dead)
            {
                playerManagement.totalPlayers--;
                dead = true;
            }
            
            if (isServer && GetComponent<PlayerControl>())
            {
                GetComponent<RefrenceKeeperAI>().updateUI.deadMessageServer.SetActive(true);
            }
            if (Input.GetKey("f") && GetComponent<PlayerControl>() && hasAuthority)
            {
                if (!isServer)
                {
                    manager.StopClient();
                }
                else
                {
                    manager.StopClient();
                    manager.StopServer();
                }
            }
        }
    }
}
