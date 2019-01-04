﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageDealer : MonoBehaviour {

    public float damage;
    public bool onServer = false;
    public bool parent = false;
    private GameObject destroyThis;
    public GameObject localRelay;

    void Start()
    {
        localRelay = GameObject.Find("LocalRelay");
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Breakable")
        {
            destroyThis = collisionInfo.gameObject;
            StartCoroutine("Destroyer");
        }
        if (onServer)
        {
            if (collisionInfo.gameObject.layer == 15)
            {
                if (collisionInfo.transform.parent.gameObject.name == "Local")
                {
                    localRelay.GetComponent<Health>().CmdUpdateHealth(damage);
                    onServer = false;
                    return;
                }
                else if (collisionInfo.transform.parent.gameObject.name == "PositionRelay(Clone)")
                {
                    collisionInfo.transform.parent.gameObject.GetComponent<Health>().CmdUpdateHealth(damage);
                    onServer = false;
                    return;
                }
                else if (collisionInfo.transform.parent.parent.gameObject.name == "Local")
                {
                    localRelay.GetComponent<Health>().CmdUpdateHealth(damage);
                    onServer = false;
                    return;
                }
                else if (collisionInfo.transform.parent.parent.gameObject.name == "PositionRelay(Clone)")
                {
                    collisionInfo.transform.parent.parent.gameObject.GetComponent<Health>().CmdUpdateHealth(damage);
                    onServer = false;
                    return;
                }
            }
        }
    }

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(destroyThis);
    }
}
