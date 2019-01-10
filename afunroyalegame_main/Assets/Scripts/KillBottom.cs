using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBottom : MonoBehaviour {

    public bool kill = true;

    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.gameObject.name == "KillLine" && kill)
        {
            Destroy(gameObject);
        }
    }
}
