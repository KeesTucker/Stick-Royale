using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollowTransformAI : MonoBehaviour {

    public Transform playert;

    void Start()
    {
        playert = GetComponent<PlayerSetupAI>().parent.transform.GetChild(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = playert.position;
    }
}
