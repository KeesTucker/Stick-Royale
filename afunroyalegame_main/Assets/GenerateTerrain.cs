using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour {

    public GameObject[] chunks;

    public float currentPosition = 0;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < chunks.Length; i++)
        {
            currentPosition -= chunks[i].GetComponent<ChunkData>().width;
        }
        currentPosition = currentPosition / 2;
        for (int i = 0; i < chunks.Length; i++)
        {
            GameObject chunk = Instantiate(chunks[i], new Vector3(currentPosition, 0, 0), Quaternion.identity);
            currentPosition += chunk.GetComponent<ChunkData>().width;
            chunk.transform.parent = transform;
        }
	}
}
