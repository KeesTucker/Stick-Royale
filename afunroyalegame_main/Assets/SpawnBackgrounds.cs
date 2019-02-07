using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBackgrounds : MonoBehaviour {

    public GameObject backGround;

    public bool inLoadScene = false;

	// Use this for initialization
	void Start () {
        if (!inLoadScene)
        {
            GameObject back = Instantiate(backGround, new Vector3(0, 0, 100), Quaternion.identity);
            back.GetComponent<MoveBackground>().cam = transform;
        }
	}
}
