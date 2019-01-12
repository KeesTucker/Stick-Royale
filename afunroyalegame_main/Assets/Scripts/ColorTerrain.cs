using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTerrain : MonoBehaviour {

    public Biome biome;
    public int type;

    public void color()
    {
        if (type == 0)
        {
            foreach (SpriteRenderer sr in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                if (sr.gameObject.name == "Top")
                {
                    sr.color = biome.grassColor;
                }
                else if (sr.gameObject.name == "Base")
                {
                    sr.color = biome.baseColor;
                }
            }
        }
        else if (type == 1)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = biome.grassColor;
        }
        else if (type == 2)
        {
            foreach (SpriteRenderer spriteRenderer in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                if (spriteRenderer.transform.parent.gameObject.tag == "TreeTop")
                {
                    spriteRenderer.color = biome.grassColor;
                }
                else
                {
                    spriteRenderer.color = biome.trunkColor;
                }
            }
        }
        if (type == 3)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = biome.rockColor;
        }
    }
}
