using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustMaker : MonoBehaviour {

    public GameObject particle;

    public bool foot;
    public AudioSource audioSource;
    public AudioClip footstep;

    void OnCollisionEnter(Collision collsionInfo)
    {
        if (collsionInfo.collider.gameObject.layer == 12)
        {
            if (foot)
            {
                audioSource.PlayOneShot(footstep);
            }
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
