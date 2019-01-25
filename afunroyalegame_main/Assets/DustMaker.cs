using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustMaker : MonoBehaviour {

    public GameObject particle;

    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
