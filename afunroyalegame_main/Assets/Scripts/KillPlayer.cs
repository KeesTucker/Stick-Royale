using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class KillPlayer : NetworkBehaviour {

    public float damage = 100000;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (isServer)
        {
            if (collisionInfo.gameObject.layer == 24)
            {
                if (collisionInfo.gameObject.tag == "PosRelay")
                {
                    collisionInfo.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    return;
                }
                else if (collisionInfo.transform.parent.gameObject.tag == "PosRelay")
                {
                    collisionInfo.transform.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    return;
                }
                else if (collisionInfo.transform.parent.parent.gameObject.tag == "PosRelay")
                {
                    collisionInfo.transform.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    return;
                }
                else if (collisionInfo.transform.parent.parent.parent.gameObject.tag == "PosRelay")
                {
                    collisionInfo.transform.parent.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    return;
                }
            }
        }
    }
}
