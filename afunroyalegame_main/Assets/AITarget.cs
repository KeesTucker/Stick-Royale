using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITarget : MonoBehaviour {

    public SpawnRocketAI spawnRocket;
    public RefrenceKeeperAI refrenceKeeper;
    public SpawnWeapons spawnWeapons;

    public bool selectedStart = false;

    public Transform closestWeapon;

    public Transform closestPlayer;

    public Transform closestGrapple;

    public GameObject parent;

    public float radius = 30f;

    private float minWeaponDistance;
    private float minPlayerDistance;
    private float minGrappleDistance;

    public bool startCheckW = true;
    public bool startCheckP = true;
    public bool startCheckG = true;

    public List<GameObject> bulletTypes = new List<GameObject>();

    public int targetType;

    public bool isServer;

    public BaseControl baseControl;

    private bool shotty = false;

    public GameObject[] typeGun;

    public Transform weapon;

    private RaycastHit hit;

    public float offset;

    public HealthAI health;

    // Bit shift the index of the layer (8) to get a bit mask
    int layerMask = 1 << 24;

    int layerMaskGround = 1 << 12;

    // Use this for initialization
    void Start () {
        isServer = parent.GetComponent<AISetup>().isServer;
        spawnWeapons = GameObject.Find("Items").GetComponent<SpawnWeapons>();
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(parent.transform.position, radius);
        if (closestGrapple)
        {
            Gizmos.DrawSphere(closestGrapple.position, 3f);
        }
        if (closestWeapon)
        {
            Gizmos.DrawSphere(closestWeapon.position, 3f);
        }
        if (closestPlayer)
        {
            Gizmos.DrawSphere(closestPlayer.position, 3f);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4f);
    }

	// Update is called once per frame
	void Update () {
        if (isServer)
        {
            if (!spawnRocket.destroyed && !selectedStart)
            {
                GameObject[] weapons = GameObject.FindGameObjectsWithTag("WeaponItem");
                targetGO(weapons[Random.Range(0, weapons.Length - 1)]);
                selectedStart = true;
            }

            startCheckG = true;
            startCheckP = true;
            startCheckW = true;

            foreach (Collider objectCol in Physics.OverlapSphere(parent.transform.position, radius))
            {
                if (objectCol.gameObject.GetComponent<AISetup>() && objectCol.gameObject != parent)
                {
                    if (startCheckP)
                    {
                        minPlayerDistance = Vector3.Distance(objectCol.transform.position, parent.transform.position);
                        closestPlayer = objectCol.transform;
                        startCheckP = false;
                    }
                    else if (Vector3.Distance(objectCol.transform.position, parent.transform.position) < minPlayerDistance)
                    {
                        minPlayerDistance = Vector3.Distance(objectCol.transform.position, parent.transform.position);
                        closestPlayer = objectCol.transform;
                    }
                }
                else if (objectCol.gameObject.tag == "WeaponItem")
                {
                    if (startCheckW)
                    {
                        minWeaponDistance = Vector3.Distance(objectCol.transform.position, parent.transform.position);
                        closestWeapon = objectCol.transform;
                        startCheckW = false;
                    }
                    else if (Vector3.Distance(objectCol.transform.position, parent.transform.position) < minWeaponDistance)
                    {
                        minWeaponDistance = Vector3.Distance(objectCol.transform.position, parent.transform.position);
                        closestWeapon = objectCol.transform;
                    }
                }
                else if (objectCol.gameObject.tag == "TerrainPart" || objectCol.gameObject.tag == "NoAttract")
                {
                    if (startCheckG)
                    {
                        minGrappleDistance = Vector3.Distance(objectCol.transform.position, parent.transform.position);
                        closestGrapple = objectCol.transform;
                        startCheckG = false;
                    }
                    else if (Vector3.Distance(objectCol.transform.position, parent.transform.position) < minGrappleDistance)
                    {
                        minGrappleDistance = Vector3.Distance(objectCol.transform.position, parent.transform.position);
                        closestGrapple = objectCol.transform;
                    }
                }
                if (startCheckG)
                {
                    closestGrapple = null;
                }
                else if (startCheckW)
                {
                    closestWeapon = null;
                }
                else if (startCheckP)
                {
                    closestPlayer = null;
                }
            }

            if (refrenceKeeper.inventoryCount < 3 && closestWeapon)
            {
                if (minWeaponDistance < minPlayerDistance && !bulletTypes.Contains(spawnWeapons.Weapons[closestWeapon.GetComponent<WeaponIndexHolder>().WeaponIndex].WeaponItem.bullet)) //Make sure not picking up same weapontype
                {
                    transform.position = closestWeapon.transform.position;
                    targetType = 0;
                }
                else if (closestPlayer)
                {
                    transform.position = closestPlayer.transform.position;
                    targetType = 1;
                }
            }
            else if (closestPlayer)
            {
                transform.position = closestPlayer.transform.position;
                targetType = 1;
            }
            else {
                transform.position = new Vector3(0, transform.position.y, 0);
            }
        }
        if (targetType == 1)
        {
            if (minPlayerDistance < 35) //Melee Distance
            {
                for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
                {
                    if (refrenceKeeper.weaponInventory[i].bullet)
                    {
                        if (refrenceKeeper.weaponInventory[i].bullet.name == "Slugs") //Switch to shotty
                        {
                            shotty = true;
                            if (refrenceKeeper.activeSlot != i)
                            {
                                if (i == 0)
                                {
                                    baseControl.one = true;
                                }
                                else if (i == 1)
                                {
                                    baseControl.two = true;
                                }
                                else if (i == 2)
                                {
                                    baseControl.three = true;
                                }
                            }
                        }
                        else if (!shotty) //Use punch
                        {
                            baseControl.four = true;
                        }
                    }
                }
            }
            else if (minPlayerDistance < 60 && bulletTypes.Contains(typeGun[1]))
            {
                for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
                {
                    if (refrenceKeeper.weaponInventory[i].bullet)
                    {
                        if (refrenceKeeper.weaponInventory[i].bullet.name == "Small Bullet") //Switch gun to shortrange
                        {
                            if (refrenceKeeper.activeSlot != i)
                            {
                                if (i == 0)
                                {
                                    baseControl.one = true;
                                }
                                else if (i == 1)
                                {
                                    baseControl.two = true;
                                }
                                else if (i == 2)
                                {
                                    baseControl.three = true;
                                }
                            }
                        }
                    }
                }
            }
            else if (minPlayerDistance < 100 && bulletTypes.Contains(typeGun[2]))
            {
                for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
                {
                    if (refrenceKeeper.weaponInventory[i].bullet)
                    {
                        if (refrenceKeeper.weaponInventory[i].bullet.name == "Medium Bullet") //Switch gun to midrange
                        {
                            if (refrenceKeeper.activeSlot != i)
                            {
                                if (i == 0)
                                {
                                    baseControl.one = true;
                                }
                                else if (i == 1)
                                {
                                    baseControl.two = true;
                                }
                                else if (i == 2)
                                {
                                    baseControl.three = true;
                                }
                            }
                        }
                    }
                }
            }
            else if (minPlayerDistance < 150 && bulletTypes.Contains(typeGun[3]))
            {
                for (int i = 0; i < refrenceKeeper.weaponInventory.Count; i++)
                {
                    if (refrenceKeeper.weaponInventory[i].bullet)
                    {
                        if (refrenceKeeper.weaponInventory[i].bullet.name == "Heavy Bullet") //Switch gun to midrange
                        {
                            if (refrenceKeeper.activeSlot != i)
                            {
                                if (i == 0)
                                {
                                    baseControl.one = true;
                                }
                                else if (i == 1)
                                {
                                    baseControl.two = true;
                                }
                                else if (i == 2)
                                {
                                    baseControl.three = true;
                                }
                            }
                        }
                    }
                }
            }
            else if (minPlayerDistance < 100)
            {
                if (refrenceKeeper.inventoryCount > 0)
                {
                    int i = refrenceKeeper.inventoryCount - 1;
                    if (i != refrenceKeeper.activeSlot)
                    {
                        if (i == 0)
                        {
                            baseControl.one = true;
                        }
                        else if (i == 1)
                        {
                            baseControl.two = true;
                        }
                        else if (i == 2)
                        {
                            baseControl.three = true;
                        }
                    }
                }
                else if (refrenceKeeper.activeSlot != 3)
                {
                    baseControl.four = true;
                }
            }
            Debug.DrawRay(weapon.position, -Vector3.Normalize(new Vector3(weapon.position.x - transform.position.x, weapon.position.y - transform.position.y, 0)) * (minPlayerDistance - 5f), Color.red);
            if (refrenceKeeper.weaponHeld && !Physics.Raycast(weapon.position, -Vector3.Normalize(new Vector3(weapon.position.x - transform.position.x, weapon.position.y - transform.position.y, 0)), out hit, minPlayerDistance - 5f, layerMask))
            {
                baseControl.lClick = true;
            }
            else if (refrenceKeeper.weaponHeld)
            {
                baseControl.lClick = false;
            }

            if (minPlayerDistance < 35 && !refrenceKeeper.weaponHeld)
            {
                if (!baseControl.lClick)
                {
                    StartCoroutine("punchKill");
                }
            }
        }

        else if (targetType == 0)
        {
            if (minWeaponDistance < 15f)
            {
                bulletTypes.Add(spawnWeapons.Weapons[closestWeapon.GetComponent<WeaponIndexHolder>().WeaponIndex].WeaponItem.bullet);
                baseControl.e = true;
            }
        }

        //Movement
        if (transform.position.x > parent.transform.position.x)
        {
            offset = (400 - health.health) / 400 * 150;
        }
        else if (transform.position.x < parent.transform.position.x)
        {
            offset = (-400 + health.health) / 400 * 150;
        }

        if (targetType == 0 || targetType == 2)
        {
            if (transform.position.x > parent.transform.position.x)
            {
                baseControl.d = true;
                baseControl.a = false;
                Debug.DrawRay(parent.transform.position, Vector3.right * 5f, Color.green);
                if (if (Physics.Raycast(parent.transform.position, Vector3.right, out hit, 5f, layerMaskGround) || Physics.Raycast(parent.transform.position, -Vector3.right, out hit, 5f, layerMaskGround)))
                {
                    baseControl.space = true;
                }
            }
            else if (transform.position.x < parent.transform.position.x)
            {
                baseControl.a = true;
                baseControl.d = false;
                Debug.DrawRay(parent.transform.position, -Vector3.right * 5f, Color.green);
                if (if (Physics.Raycast(parent.transform.position, Vector3.right, out hit, 5f, layerMaskGround) || Physics.Raycast(parent.transform.position, -Vector3.right, out hit, 5f, layerMaskGround)))
                {
                    baseControl.space = true;
                }
            }
        }
        else if (targetType == 1)
        {
            if (Random.Range(0, (int)(1 / Time.deltaTime)) == 1) 
            {
                baseControl.space = true;
            }
            if (transform.position.x - offset > parent.transform.position.x)
            {
                baseControl.d = true;
                baseControl.a = false;
                Debug.DrawRay(parent.transform.position, Vector3.right * 15f, Color.green);
                if (Physics.Raycast(parent.transform.position, Vector3.right, out hit, 5f, layerMaskGround) || Physics.Raycast(parent.transform.position, -Vector3.right, out hit, 5f, layerMaskGround))
                {
                    baseControl.space = true;
                }
            }
            else if (transform.position.x - offset < parent.transform.position.x)
            {
                baseControl.a = true;
                baseControl.d = false;
                Debug.DrawRay(parent.transform.position, -Vector3.right * 15f, Color.green);
                if (Physics.Raycast(parent.transform.position, -Vector3.right, out hit, 5f, layerMaskGround) || Physics.Raycast(parent.transform.position, Vector3.right, out hit, 5f, layerMaskGround))
                {
                    baseControl.space = true;
                }
            }
        }
    }

    public void targetGO(GameObject target)
    {
        transform.position = target.transform.position;
    }

    IEnumerator punchKill()
    {
        baseControl.lClick = true;
        yield return new WaitForSeconds(0.2f);
        baseControl.lClick = false;
    }
}
