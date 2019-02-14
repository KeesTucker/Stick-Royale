using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleActivatorAI : MonoBehaviour {

    public Rigidbody rb;
    public GameObject grapple;
    public GameObject hand;
    public bool Collided;
    public float angle;
    public float angleTarg;
    public GameObject[] playerParts;
    public Vector3 hitCoords;
    public GameObject hitTarg;
    public bool backTime = false;
    public Vector3 direction;
    public Quaternion rotation;
    public GameObject handSingle;
    public Vector3 directionTarg;
    public Quaternion rotationTarg;
    public Renderer rend;
    public bool onLocal;
    public bool attractable = true;
    public AimShootAI aimShoot;
    public SyncMoveStateAI syncMoveState;
    public Material invisible;
    public GameObject parent; //AI that sent it
    public GameObject particle;
    void Start()
    {
        GameObject[] playerParts_ = parent.GetComponent<GroundForceAI>().playerParts;
        StartCoroutine("endTime");
        aimShoot = parent.transform.GetChild(0).gameObject.GetComponent<AimShootAI>();
    }
    public void Setup(Color syncedC)
    {
        rend.material.SetColor("_Color", syncedC);
    }

    void OnCollisionEnter(Collision collsionInfo)
    {
        //if (collsionInfo.collider.gameObject.layer != 15/* && backTime == false*/)
        //{
        if (collsionInfo.gameObject.tag == "NoGrapple")
        {
            backTime = true;
            return;
        }
        else if (collsionInfo.gameObject.tag == "NoAttract")
        {
            attractable = false;
        }
        grapple.layer = 14;
        hitTarg = collsionInfo.gameObject;
        hitCoords = transform.position - collsionInfo.gameObject.transform.position;
        rb.isKinematic = true;
        Collided = true;
        if (onLocal)
        {
            parent.GetComponent<GroundForceAI>().grappled = true;
        }
        transform.position = hitTarg.transform.position + hitCoords;
        transform.parent = hitTarg.transform;

        Instantiate(particle, transform.position, Quaternion.identity);

        rend.material = invisible;
        //}
    }

    void Update()
    {
        if (parent.GetComponent<PlayerControl>())
        {
            if (parent.GetComponent<PlayerControl>().e)
            {
                if (onLocal)
                {
                    parent.GetComponent<GroundForceAI>().grappled = false;
                }
                Destroy(grapple);
            }
        }
        else if (parent.GetComponent<BaseControl>())
        {
            if (parent.GetComponent<BaseControl>().e)
            {
                if (onLocal)
                {
                    parent.GetComponent<GroundForceAI>().grappled = false;
                }
                Destroy(grapple);
            }
        }
    }

    void FixedUpdate()
    {
        if (!backTime && !Collided)
        {
            if (rb.velocity.magnitude < 100)
            {
                rb.AddForce(rb.velocity.normalized * (100000 / 50) * Time.deltaTime);
            }
        }
        angle = AngleBetweenPoints(hand.GetComponent<Transform>().position, transform.position);
        rotation = Quaternion.Euler(0, 0, angle);
        direction = rotation * -Vector3.right;
        if (Collided)
        {
            angleTarg = AngleBetweenPoints(hitTarg.transform.position, hand.GetComponent<Transform>().position);
            rotationTarg = Quaternion.Euler(0, 0, angleTarg);
            directionTarg = rotationTarg * -Vector3.right;
        }
        handSingle = hand;
        if (Collided)
        {
            //transform.position = hitTarg.transform.position + hitCoords;
            if (Vector3.Distance(handSingle.transform.position, hitTarg.transform.position) > 3)
            {
                handSingle.GetComponent<Rigidbody>().AddForce(direction * 40000 * Time.deltaTime/* * Mathf.Pow(Vector3.Distance(handSingle.transform.position, hitTarg.transform.position), 2f)*/);

                if (hitTarg.GetComponent<Rigidbody>() != null && attractable)
                {
                    hitTarg.GetComponent<Rigidbody>().AddForce(directionTarg * 30000 * Time.deltaTime/* * Mathf.Pow(Vector3.Distance(handSingle.transform.position, hitTarg.transform.position), 2f)*/);
                }
            }
        }
        if (backTime)
        {
            rb.AddForce(-direction * (90000 / 25) * Time.deltaTime);
            if (Vector3.Distance(handSingle.transform.position, transform.position) < 2)
            {
                if (onLocal)
                {
                    parent.GetComponent<GroundForceAI>().grappled = false;
                }
                Destroy(grapple);
            }
        }
    }

    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    IEnumerator endTime()
    {
        yield return new WaitForSeconds(0.47f);
        if (Collided == false)
        {
            backTime = true;
        }
    }
}

