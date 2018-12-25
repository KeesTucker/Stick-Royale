using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour {

    [SyncVar]
    public float health = 200;
    GameObject[] RotHolders;
	
	// Update is called once per frame
	void Update () {
        if (health <= 0 && isLocalPlayer)
        {
            CmdDestroyOnServer(gameObject);
            Destroy(GameObject.Find("Local"));

            RotHolders = GameObject.FindGameObjectsWithTag("RagAng");
            foreach(GameObject RotHolder in RotHolders)
            {
                if (RotHolder.GetComponent<SyncRotation>().parent == gameObject)
                {
                    CmdDestroyOnServer(RotHolder);
                }
            }
            //spawn ghost here
        }
	}

    [Command]
    public void CmdUpdateHealth(float damage)
    {
        health -= damage;
    }

    [Command]
    public void CmdDestroyOnServer(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
