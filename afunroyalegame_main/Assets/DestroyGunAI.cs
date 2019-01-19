using UnityEngine;
using Mirror;

public class DestroyGunAI : NetworkBehaviour
{

    RefrenceKeeperAI refrenceKeeper;

    void Start()
    {
        refrenceKeeper = GetComponent<PlayerSetupAI>().parent.GetComponent<RefrenceKeeperAI>();
    }

    [Command]
    public void CmdDestroyGun(GameObject gun, int id)
    {
        gun.GetComponent<Pickup>().deactivated = true;
        Debug.Log("destroy");
        RpcDestroyGun(gun, id);
        Destroy(gun);
    }

    [ClientRpc]
    void RpcDestroyGun(GameObject gun, int id)
    {
        GameObject.Find("Local").GetComponent<RefrenceKeeper>().itemDistanceRefrences[id - 1] = 20;
        Debug.Log("destroy");
        Destroy(gun);
    }
}
