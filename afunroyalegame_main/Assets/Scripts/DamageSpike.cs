using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpike : MonoBehaviour {

    public float damage;
    public GameObject localRelay;
    public bool damagable = true;

    IEnumerator OnCollisionEnter(Collision collisionInfo)
    {
        if (!localRelay)
        {
            localRelay = GameObject.Find("LocalRelay");
        }
        
        if (collisionInfo.gameObject.layer == 15 && damagable)
        {
            if (collisionInfo.transform.parent.gameObject.name == "Local")
            {
                localRelay.GetComponent<Health>().CmdUpdateHealth(damage);
                damagable = false;
                yield return new WaitForSeconds(0.5f);
                damagable = true;
            }
            else if (collisionInfo.transform.parent.parent.gameObject.name == "Local")
            {
                damagable = false;
                localRelay.GetComponent<Health>().CmdUpdateHealth(damage);
                yield return new WaitForSeconds(0.5f);
                damagable = true;
            }
            else if (collisionInfo.transform.parent.parent.parent.gameObject.name == "Local")
            {
                damagable = false;
                localRelay.GetComponent<Health>().CmdUpdateHealth(damage);
                yield return new WaitForSeconds(0.5f);
                damagable = true;
            }
        }
        if (collisionInfo.gameObject.layer == 24 && damagable)
        {
            if (collisionInfo.transform.parent.gameObject.tag == "PosRelay")
            {
                collisionInfo.transform.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                damagable = false;
                yield return new WaitForSeconds(0.5f);
                damagable = true;
            }
            else if (collisionInfo.transform.parent.parent.gameObject.tag == "PosRelay")
            {
                damagable = false;
                collisionInfo.transform.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                yield return new WaitForSeconds(0.5f);
                damagable = true;
            }
            else if (collisionInfo.transform.parent.parent.parent.gameObject.tag == "PosRelay")
            {
                damagable = false;
                collisionInfo.transform.parent.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                yield return new WaitForSeconds(0.5f);
                damagable = true;
            }
        }
    }
}
