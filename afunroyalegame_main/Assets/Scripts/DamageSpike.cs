using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpike : MonoBehaviour {

    public float damage;
    public bool damagable = true;

    IEnumerator OnCollisionEnter(Collision collisionInfo)
    {
        if (damagable)
        {
            if (collisionInfo.gameObject.tag == "PosRelay")
            {
                collisionInfo.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                damagable = false;
                yield return new WaitForSeconds(0.5f);
                damagable = true;
            }
            else if (collisionInfo.transform.parent.gameObject.tag == "PosRelay")
            {
                collisionInfo.transform.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                damagable = false;
                yield return new WaitForSeconds(0.5f);
                damagable = true;
            }
            else if (collisionInfo.transform.parent.parent.gameObject.tag == "PosRelay")
            {
                collisionInfo.transform.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                damagable = false;
                yield return new WaitForSeconds(0.5f);
                damagable = true;
            }
            else if (collisionInfo.transform.parent.parent.parent.gameObject.tag == "PosRelay")
            {
                collisionInfo.transform.parent.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                damagable = false;
                yield return new WaitForSeconds(0.5f);
                damagable = true;
            }
        }
    }
}
