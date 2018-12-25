using UnityEngine;
using Mirror;

public class DestroyGun : NetworkBehaviour {

    RefrenceKeeper refrenceKeeper;

    void Start()
    {
        refrenceKeeper = GameObject.Find("Local").GetComponent<RefrenceKeeper>();
    }

    [Command]
    public void CmdDestroyGun(GameObject gun, int id)
    {
        Debug.Log("destroy");
        RpcDestroyGun(gun, id);
        refrenceKeeper.itemDistanceRefrences[id - 1] = 20;
        Destroy(gun);
    }

    [ClientRpc]
    void RpcDestroyGun(GameObject gun, int id)
    {
        Debug.Log("destroy");
        refrenceKeeper.itemDistanceRefrences[id - 1] = 20;
        Destroy(gun);
    }
}
