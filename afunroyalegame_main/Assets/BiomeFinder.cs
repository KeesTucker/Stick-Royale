using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeFinder : MonoBehaviour {

    GenerateTerrain generateTerrain;
    bool run = true;

	// Use this for initialization
	void Start () {
        generateTerrain = GetComponent<GenerateTerrain>();
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.childCount == generateTerrain.size && run)
        {
            float width = generateTerrain.size / generateTerrain.Biomes.Count;
            if (width < 1)
            {
                width = 1f;
            }
            int widthInt = (int)width;

            int biomeCount = 0;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetChild(0).GetComponent<BiomeHolder>().biomeIndex = (int)(biomeCount / width);
                transform.GetChild(i).GetChild(0).GetComponent<BiomeHolder>().GetBiome();
                biomeCount++;
            }
            run = false;
        }
	}
}
