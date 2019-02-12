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

    public bool isPlayer;

    public GameObject Ghost;
	
	void Start()
    {
        refrenceKeeper = GetComponent<RefrenceKeeperAI>();
        if (GetComponent<AISetup>().isServer)
        {
            health = SyncData.health;
        }
        else
        {
            health = GameObject.Find("Player(Clone)").GetComponent<HealthAI>().health;
        }
    }

    void Update()
    {
        if (health <= 0 && hasAuthority && !deaded)
        {
            StartCoroutine("DestroyPlayer");
            deaded = true;
        }
    }

    public IEnumerator DestroyPlayer()
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
        if (hasAuthority && GetComponent<PlayerControl>())
        {
            refrenceKeeper.updateUI.deadMessage.SetActive(true);
            refrenceKeeper.updateUI.deadPanel.SetActive(true);
            yield return new WaitForSeconds(3f);
            refrenceKeeper.updateUI.deadPanel.SetActive(false);
        }
        transform.Find("Physics AnimatorAI").GetComponent<PlayerMovementAI>().enabled = false;
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

        foreach (HingeJoint hj in GetComponentsInChildren<HingeJoint>())
        {
            hj.useSpring = false;
        }
        if (isPlayer)
        {
            CmdSpawnGhost();
        }
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
            foreach (HingeJoint hj in GetComponentsInChildren<HingeJoint>())
            {
                hj.useSpring = false;
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
            foreach (HingeJoint hj in GetComponentsInChildren<HingeJoint>())
            {
                hj.useSpring = false;
            }
            RpcDestroyPlayer();
        }
    }

    [Command]
    public void CmdUpdateHealth(float damage)
    {
        health -= damage;
    }
}
