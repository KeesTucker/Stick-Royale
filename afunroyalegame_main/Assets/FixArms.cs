using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixArms : MonoBehaviour {

    public Transform[] limbs;
    public Vector3[] initialPos;
    public bool hasAuthority;
    public int frames;
    SpawnRocketAI spawnRocketAI;

    void Start ()
    {
        spawnRocketAI = GetComponent<SpawnRocketAI>();
        frames = Random.Range(0, 30);
        if (GetComponent<AISetup>())
        {
            hasAuthority = GetComponent<AISetup>().hasAuthority;
        }
        
        for (int i = 0; i < limbs.Length; i++)
        {
            initialPos[i] = new Vector3(limbs[i].localPosition.x, limbs[i].localPosition.y, 0);
        }
    }

	void LateUpdate () {
        if (spawnRocketAI)
        {
            if (hasAuthority || !spawnRocketAI.destroyed)
            {
                for (int i = 0; i < limbs.Length; i++)
                {
                    //if (Mathf.Abs(limbs[0].localPosition.x - initialPos[]) > )
                    //{
                    limbs[i].localPosition = initialPos[i];
                    //}
                }
            }
            else
            {
                frames++;
                if (frames >= 100)
                {
                    for (int i = 0; i < limbs.Length; i++)
                    {
                        //if (Mathf.Abs(limbs[0].localPosition.x - initialPos[]) > )
                        //{
                        limbs[i].localPosition = initialPos[i];
                        //}
                    }
                    frames = 0;
                }
                Debug.Log("ISTHISWORKING");
            }
        }
        else
        {
            for (int i = 0; i < limbs.Length; i++)
            {
                //if (Mathf.Abs(limbs[0].localPosition.x - initialPos[]) > )
                //{
                limbs[i].localPosition = initialPos[i];
                //}
            }
        }
	}
}
