﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixArms : MonoBehaviour {

    public Transform[] limbs;
    public Vector3[] initialPos;

    void Start ()
    {
        for (int i = 0; i < limbs.Length; i++)
        {
            initialPos[i] = new Vector3(limbs[i].localPosition.x, limbs[i].localPosition.y, 0);
        }
    }

	void LateUpdate () {
        for (int i = 0; i < limbs.Length; i++)
        {
            //if (Mathf.Abs(limbs[0].localPosition.x - initialPos[]) > )
            //{
                limbs[i].localPosition = initialPos[i];
            //}
        }
	}
}
