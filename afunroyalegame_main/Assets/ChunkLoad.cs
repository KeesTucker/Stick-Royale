using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ChunkLoad : NetworkBehaviour {

    public float centre;
    public Transform local;
    public float distance;
    public bool last = true;

    public int cullD = 500;
    //Renderer r;
	// Use this for initialization
	void Start () {
        transform.parent = GameObject.Find("Terrain").transform;
        local = GameObject.FindGameObjectsWithTag("Ragdoll")[0].transform;
        //r = GetComponent<Renderer>();
        centre = transform.GetChild(0).position.x + transform.GetChild(0).gameObject.GetComponent<ChunkData>().width / 2;
    }
	
	// Update is called once per frame
	void Update () {
        if (!local)
        {
            local = GameObject.FindGameObjectsWithTag("Ragdoll")[0].transform;
        }
        distance = Mathf.Abs(centre - local.position.x);
        if (distance > cullD && last)
        {
            foreach (Transform child in transform.GetChild(0).GetComponentsInChildren<Transform>())
            {
                if (!child.gameObject.name.Contains("Chunk"))
                {
                    if (child.gameObject.GetComponent<SpriteRenderer>())
                    {
                        child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    if (child.gameObject.GetComponent<Movable>() || !isServer)
                    {
                        child.gameObject.SetActive(false);
                    }
                    else if (isServer && !child.gameObject.GetComponent<Collider>())
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
            last = false;
        }
        else if (distance < cullD && !last)
        {
            foreach (Transform child in transform.GetComponentsInChildren<Transform>())
            {
                if (child.gameObject.GetComponent<SpriteRenderer>())
                {
                    child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
                child.gameObject.SetActive(true);
            }
            last = true;
        }
	}
}
