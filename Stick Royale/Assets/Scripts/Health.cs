using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour {

    [SyncVar]
    public float health = 200;
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            //spawn ghost here
        }
	}

    [Command]
    public void CmdUpdateHealth(float damage)
    {
        health -= damage;
    }
}
