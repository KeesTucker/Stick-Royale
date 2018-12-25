using UnityEngine;

public class DamageDealer : MonoBehaviour {

    public float damage;
    public bool onServer = false;
    public bool parent = false;
    public GameObject localRelay;

    void Start()
    {
        localRelay = GameObject.Find("LocalRelay");
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (onServer)
        {
            if (collisionInfo.gameObject.layer == 15)
            {
                if (collisionInfo.transform.parent.gameObject.name == "Local")
                {
                    localRelay.GetComponent<Health>().CmdUpdateHealth(damage);
                    onServer = false;
                    return;
                }
                else if (collisionInfo.transform.parent.gameObject.name == "PositionRelay(Clone)")
                {
                    collisionInfo.transform.parent.gameObject.GetComponent<Health>().CmdUpdateHealth(damage);
                    onServer = false;
                    return;
                }
                else if (collisionInfo.transform.parent.parent.gameObject.name == "Local")
                {
                    localRelay.GetComponent<Health>().CmdUpdateHealth(damage);
                    onServer = false;
                    return;
                }
                else if (collisionInfo.transform.parent.parent.gameObject.name == "PositionRelay(Clone)")
                {
                    collisionInfo.transform.parent.parent.gameObject.GetComponent<Health>().CmdUpdateHealth(damage);
                    onServer = false;
                    return;
                }
            }
        }
    }
}
