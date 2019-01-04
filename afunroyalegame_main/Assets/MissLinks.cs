using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissLinks : MonoBehaviour {

	public void Miss(GameObject[] links)
    {
        for (int i = 0; i < links.Length; i++)
        {
            Physics.IgnoreCollision(links[i].GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
