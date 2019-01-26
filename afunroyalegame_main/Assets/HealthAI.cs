using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class HealthAI : NetworkBehaviour {
    [SyncVar]
    public float health = 200;

    public GameObject weapon;

    public GameObject weaponItem;

    public RefrenceKeeperAI refrenceKeeper;

    public bool deaded;

    public GameObject Ghost;
	
	void Start()
    {
        refrenceKeeper = GetComponent<RefrenceKeeperAI>();
    }

    void Update()
    {
        if (health <= 0 && hasAuthority && !deaded)
        {
            DestroyPlayer();
            deaded = true;
        }
    }

    public void DestroyPlayer()
    {
        CmdDestroyPlayer();
        GetComponent<GroundForceAI>().grappled = true;
        if (GetComponent<PlayerControl>())
        {
            GetComponent<PlayerControl>().enabled = false;
        }
        else if (GetComponent<BaseControl>())
        {
            GetComponent<BaseControl>().enabled = false;
        }
        for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
        {
            int id = refrenceKeeper.weaponInventory[i].id;
            if (hasAuthority)
            {
                GetComponent<SpawnItem>().CmdSpawnKilled(weaponItem, transform.position, id, 0, refrenceKeeper.weaponInventory[i].currentBullets);
            }
        }
        refrenceKeeper.weaponInventory.Clear();
        refrenceKeeper.itemInventory.Clear();
        refrenceKeeper.inventoryCount = 0;
        refrenceKeeper.itemCount = 0;
        for (int i = 0; i < weapon.transform.childCount; i++)
        {
            Destroy(weapon.transform.GetChild(i).gameObject);
        }
        CmdSpawnGhost();
    }

    [Command]
    public void CmdSpawnGhost()
    {
        GameObject currentSpawn = Instantiate(Ghost, transform.position, Quaternion.identity);
        currentSpawn.GetComponent<GhostMovement>().parent = gameObject;
        NetworkServer.SpawnWithClientAuthority(currentSpawn, GetComponent<AISetup>().parent);
    }

    [ClientRpc]
    public void RpcDestroyPlayer()
    {
        if (!hasAuthority)
        {
            GetComponent<GroundForceAI>().grappled = true;
            if (GetComponent<PlayerControl>())
            {
                GetComponent<PlayerControl>().enabled = false;
            }
            else if (GetComponent<BaseControl>())
            {
                GetComponent<BaseControl>().enabled = false;
            }
            for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
            {
                int id = refrenceKeeper.weaponInventory[i].id;
                if (hasAuthority)
                {
                    GetComponent<SpawnItem>().CmdSpawnKilled(weaponItem, transform.position, id, 0, refrenceKeeper.weaponInventory[i].currentBullets);
                }
            }
            refrenceKeeper.weaponInventory.Clear();
            refrenceKeeper.itemInventory.Clear();
            refrenceKeeper.inventoryCount = 0;
            refrenceKeeper.itemCount = 0;
            for (int i = 0; i < weapon.transform.childCount; i++)
            {
                Destroy(weapon.transform.GetChild(i).gameObject);
            }
        }
    }

    [Command]
    public void CmdDestroyPlayer()
    {
        if (!hasAuthority)
        {
            GetComponent<GroundForceAI>().grappled = true;
            if (GetComponent<PlayerControl>())
            {
                GetComponent<PlayerControl>().enabled = false;
            }
            else if (GetComponent<BaseControl>())
            {
                GetComponent<BaseControl>().enabled = false;
            }
            for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
            {
                int id = refrenceKeeper.weaponInventory[i].id;
                if (hasAuthority)
                {
                    GetComponent<SpawnItem>().CmdSpawnKilled(weaponItem, transform.position, id, 0, refrenceKeeper.weaponInventory[i].currentBullets);
                }
            }
            refrenceKeeper.weaponInventory.Clear();
            refrenceKeeper.itemInventory.Clear();
            refrenceKeeper.inventoryCount = 0;
            refrenceKeeper.itemCount = 0;
            for (int i = 0; i < weapon.transform.childCount; i++)
            {
                Destroy(weapon.transform.GetChild(i).gameObject);
            }
            RpcDestroyPlayer();
        }
    }

    public void CmdUpdateHealth(float damage)
    {
        health -= damage;
    }
}
