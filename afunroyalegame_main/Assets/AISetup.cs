using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AISetup : NetworkBehaviour
{

    public Collider[] colliders;

    public GameObject parent;

    public bool local = false;

    // Use this for initialization
    void Start()
    {   
        for (int i = 0; i < colliders.Length; i++)
        {
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
}
