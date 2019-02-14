using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToSpawnList : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Find("Items").GetComponent<SpawnWeapons>().spawns.Add(gameObject);
	}
}
