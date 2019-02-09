using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnRocketAI : NetworkBehaviour
{

    public GameObject rocket;

    public GameObject rocketGO;

    public bool spaceDepressed = true;

    public Collider[] ragdollColliders;

    public SpriteRenderer[] ragdollSpriteRenderers;

    public ParticleSystem particle;

    public ParticleSystem.EmissionModule emission;

    public float timeDeath = 20f;

    public bool done = false;

    public bool AISpace = false; //ai turns this on

    public bool camActive;

    public GameObject mainCam;

    public bool destroyed = false;

    public GameObject box;
    public GameObject boxHold;

    public RefrenceKeeperAI refrenceKeeper;

    [SyncVar]
    public bool ready;

    IEnumerator Start()
    {
        rocketGO = Instantiate(rocket, transform.position, transform.rotation); //Spawn Rocket
        rocketGO.transform.GetChild(1).gameObject.SetActive(false);
        rocketGO.GetComponent<RocketMove>().ragdoll = transform;
        boxHold = Instantiate(box, transform.position, Quaternion.identity);
        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = false;
        }
        foreach (SpriteRenderer sprite in ragdollSpriteRenderers)
        {
            sprite.enabled = false;
        }
        yield return new WaitForSeconds(0.2f);
        if (gameObject.name == "PlayerLocal(Clone)" && hasAuthority)
        {
            mainCam = GameObject.Find("Main Camera(Clone)");
            mainCam.GetComponent<Camera>().orthographicSize = 100;
            camActive = true;
        }
        rocketGO.GetComponent<SpriteRenderer>().color = gameObject.GetComponent<ColourSetterAI>().m_NewColor;
        if (!isServer)
        {
            if (refrenceKeeper.updateUI)
            {
                refrenceKeeper.updateUI.fNote.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (isServer && !ready && Input.GetKey("f"))
        {
            ready = true;
            StartCoroutine("timerToKill");
            if (refrenceKeeper.updateUI)
            {
                refrenceKeeper.updateUI.fNote.SetActive(false);
            }
        }
        if (AISpace)
        {
            spaceDepressed = true;
        }
        if (spaceDepressed && !done)
        {
            foreach (Collider col in ragdollColliders)
            {
                col.enabled = true;
            }
            foreach (SpriteRenderer sprite in ragdollSpriteRenderers)
            {
                sprite.enabled = true;
            }
            rocketGO.transform.GetChild(1).gameObject.SetActive(true);
            rocketGO.transform.GetChild(0).gameObject.SetActive(false);
            rocketGO.GetComponent<SpriteRenderer>().enabled = false;
            rocketGO.GetComponent<Collider>().enabled = false;
            StartCoroutine("destroy");
            done = true;
        }
    }

    void LateUpdate()
    {
        if (boxHold)
        {
            rocketGO.transform.position = boxHold.transform.position;
        }
        if (ready)
        {
            Destroy(boxHold);
        }
    }

    IEnumerator timerToKill()
    {
        yield return new WaitForSeconds(timeDeath);
        spaceDepressed = true;
    }

    IEnumerator destroy()
    {
        if (camActive)
        {
            float size = 100;
            for (int i = 0; i < 60; i++)
            {
                mainCam.GetComponent<Camera>().orthographicSize = size;
                size--;
                yield return new WaitForEndOfFrame();
            }
        }
        yield return new WaitForSeconds(0.3f);
        destroyed = true;
        Destroy(rocketGO);
    }
}
