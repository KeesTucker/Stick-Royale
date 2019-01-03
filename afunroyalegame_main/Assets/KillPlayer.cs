using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KillPlayer : NetworkBehaviour {

    private GameObject localRelay;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.3f);
        localRelay = GameObject.Find("LocalRelay");
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (isServer)
        {
            if (collisionInfo.gameObject.layer == 15)
            {
                if (collisionInfo.transform.parent.gameObject.name == "Local")
                {
                    localRelay.GetComponent<Health>().CmdUpdateHealth(10000f);
                    return;
                }
                else if (collisionInfo.transform.parent.gameObject.name == "PositionRelay(Clone)")
                {
                    collisionInfo.transform.parent.gameObject.GetComponent<Health>().CmdUpdateHealth(10000f);
                    return;
                }
                else if (collisionInfo.transform.parent.parent.gameObject.name == "Local")
                {
                    localRelay.GetComponent<Health>().CmdUpdateHealth(10000f);
                    return;
                }
                else if (collisionInfo.transform.parent.parent.gameObject.name == "PositionRelay(Clone)")
                {
                    collisionInfo.transform.parent.parent.gameObject.GetComponent<Health>().CmdUpdateHealth(10000f);
                    return;
                }
            }
        }
    }
}
