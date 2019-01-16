using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeFinder : MonoBehaviour {

    GenerateTerrain generateTerrain;
    bool run = false;

	// Use this for initialization
	IEnumerator Start () {
        generateTerrain = GetComponent<GenerateTerrain>();
        yield return new WaitForSeconds(0.3f);
        run = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (run)
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
                if (transform.GetChild(i).GetChild(0).GetComponent<BiomeHolder>().isServer)
                {
                    transform.GetChild(i).GetChild(0).GetComponent<BiomeHolder>().biomeIndex = (int)(biomeCount / width);
                }
                
                transform.GetChild(i).GetChild(0).GetComponent<BiomeHolder>().GetBiome();
                biomeCount++;
            }
            run = false;
        }
	}
}
