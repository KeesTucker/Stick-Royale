using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageDealer : MonoBehaviour {

    public float damage;
    public bool onServer = false;
    public bool parent = false;
    private GameObject destroyThis;
    public float speed = 5f;
    public Rigidbody weapon;
    public bool isAWeapon = false;
    public GameObject particle;
    public GameObject gunParticle;
    public Rigidbody rb;
    public bool particleDone = false;
    public bool punching = false;
    public float velocityX;
    public float velocityY;
    public bool weaponParticle = false;
    public Color ouchie = new Color(0.8f, 0.05f, 0.05f);
    Color oldColor;
    public GameObject hitMarker;
    GameObject hit;

    IEnumerator Start()
    {
        rb = GetComponent<Rigidbody>();
        yield return new WaitForSeconds(0.1f);
        if (isAWeapon)
        {
            if (GetComponent<WeaponIndexHolder>().isServer)
            {
                weapon = GetComponent<Rigidbody>();
            }
            else
            {
                isAWeapon = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (weapon && isAWeapon)
        {
            damage = (int)(weapon.velocity.magnitude * 0.3f);
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "Breakable")
        {
            destroyThis = collisionInfo.gameObject;
            StartCoroutine("Destroyer");
        }

        if (collisionInfo.gameObject.layer == 24 && !particleDone && gameObject.tag != "WeaponItem" && gameObject.name != "LimbEnd")
        {
            StartCoroutine(SpawnParticle(collisionInfo));
            particleDone = true;
        }
        else if (collisionInfo.gameObject.layer == 24 && !particleDone && weaponParticle && gameObject.name != "LimbEnd")
        {
            StartCoroutine(SpawnParticle(collisionInfo));
            particleDone = true;
        }
        else if (collisionInfo.gameObject.layer == 24 && !particleDone && gameObject.tag != "WeaponItem" && punching)
        {
            StartCoroutine(SpawnParticle(collisionInfo));
            particleDone = true;
        }
        else if(gameObject.layer == 9)
        {
            StartCoroutine(SpawnParticle(collisionInfo));
        }

        if (weapon)
        {
            if (collisionInfo.gameObject.layer == 24)
            {
                if (collisionInfo.gameObject.tag == "PosRelay")
                {
                    collisionInfo.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    weapon = null;
                    return;
                }
                else if (collisionInfo.transform.parent.gameObject.tag == "PosRelay")
                {
                    collisionInfo.transform.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    weapon = null;
                    return;
                }
                else if (collisionInfo.transform.parent.parent.gameObject.tag == "PosRelay")
                {
                    collisionInfo.transform.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    weapon = null;
                    return;
                }
                else if (collisionInfo.transform.parent.parent.parent.gameObject.tag == "PosRelay")
                {
                    collisionInfo.transform.parent.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    weapon = null;
                    return;
                }
            }
        }
        else if (onServer)
        {
            if (collisionInfo.gameObject.layer == 24)
            {
                if (collisionInfo.gameObject.tag == "PosRelay")
                {
                    collisionInfo.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    onServer = false;
                    return;
                }
                else if (collisionInfo.transform.parent.gameObject.tag == "PosRelay")
                {
                    collisionInfo.transform.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    onServer = false;
                    return;
                }
                else if (collisionInfo.transform.parent.parent.gameObject.tag == "PosRelay")
                {
                    collisionInfo.transform.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    onServer = false;
                    return;
                }
                else if (collisionInfo.transform.parent.parent.parent.gameObject.tag == "PosRelay")
                {
                    collisionInfo.transform.parent.parent.parent.gameObject.GetComponent<HealthAI>().CmdUpdateHealth(damage);
                    onServer = false;
                    return;
                }
            }
        }
        isAWeapon = false;
        weaponParticle = false;
    }

    IEnumerator SpawnParticle(Collision info)
    {
        if (info.gameObject.layer == 24)
        {
            ParticleSystem.MainModule system = particle.GetComponent<ParticleSystem>().main;
            if (info.gameObject.tag == "PosRelay" || info.gameObject.name == "LimbEnd")
            {
                system.startColor = info.gameObject.GetComponent<SpriteRenderer>().color;
                GameObject ouchParticle = Instantiate(particle, info.contacts[0].point, Quaternion.identity);
                ouchParticle.transform.forward = new Vector3(-info.contacts[0].normal.x, -info.contacts[info.contacts.Length - 1].normal.y, info.contacts[info.contacts.Length - 1].normal.z);
                oldColor = info.gameObject.GetComponent<SpriteRenderer>().color;
                if (punching)
                {
                    hit = Instantiate(hitMarker, info.contacts[0].point, Quaternion.identity);
                    hit.GetComponent<SpriteRenderer>().color = oldColor;
                }
                info.gameObject.GetComponent<SpriteRenderer>().color = ouchie;
                yield return new WaitForSeconds(0.2f);
                info.gameObject.GetComponent<SpriteRenderer>().color = oldColor;
                Destroy(hit);
            }
            else
            {
                system.startColor = info.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                GameObject ouchParticle = Instantiate(particle, info.contacts[0].point, Quaternion.identity);
                ouchParticle.transform.forward = new Vector3(-info.contacts[0].normal.x, -info.contacts[info.contacts.Length - 1].normal.y, info.contacts[info.contacts.Length - 1].normal.z);
                oldColor = info.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
                if (punching)
                {
                    hit = Instantiate(hitMarker, info.contacts[0].point, Quaternion.identity);
                    hit.GetComponent<SpriteRenderer>().color = oldColor;
                }
                info.transform.GetChild(0).GetComponent<SpriteRenderer>().color = ouchie;
                yield return new WaitForSeconds(0.2f);
                info.transform.GetChild(0).GetComponent<SpriteRenderer>().color = oldColor;
                Destroy(hit);
            }
        }
        else if (gameObject.layer == 9)
        {
            GameObject particle = Instantiate(gunParticle, info.contacts[0].point, Quaternion.identity);
            particle.transform.forward = info.contacts[0].normal;
        }
    }

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(destroyThis);
    }
}
