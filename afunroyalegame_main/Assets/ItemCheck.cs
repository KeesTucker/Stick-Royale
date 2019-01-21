using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ItemCheck : NetworkBehaviour {

    public List<GameObject> items = new List<GameObject>();
    public List<float> itemDistanceRefrences = new List<float>();

    public Transform ragdoll;

    public float minDistance = 10000000;
    public GameObject closestItem;
    public int indexMin;
    public int weaponIndex;

    public bool e;

    public float pickupDistance;

    public AimShootAI aimShoot;

    public ShootAI shoot;

    public RefrenceKeeperAI refrenceKeeper;

    public SpawnWeapons spawnWeapons;

    public SwitchWeaponAI switchWeapon;

    public Transform aim;

    public float direction;

    UpdateUI updateUI;

    // Use this for initialization
    IEnumerator Start () {
        spawnWeapons = GameObject.Find("Items").GetComponent<SpawnWeapons>();
        updateUI = GameObject.Find("PlayerUI").GetComponent<UpdateUIRefrence>().updateUI;
        yield return new WaitForSeconds(0.3f);
        checkDistances();
    }
	
	// Update is called once per frame
	void Update () {
        if (e && hasAuthority)
        {
            checkDistances();
            if (minDistance < pickupDistance)
            {
                pickupItem();
            }
            e = false;
        }
        if (!closestItem)
        {
            checkDistances();
        }
	}

    public void checkDistances()
    {
        if (hasAuthority)
        {
            GameObject[] itemsArray = GameObject.FindGameObjectsWithTag("WeaponItem");
            //if (itemsArray.Length > items.Count)
            //{
            items.Clear();
            itemDistanceRefrences.Clear();
            for (int i = 0; i < itemsArray.Length; i++)
            {
                itemDistanceRefrences.Add(20);
                items.Add(itemsArray[i]);
            }
            //}
            minDistance = 100000;
            for (int i = 0; i < items.Count; i++)
            {
                itemDistanceRefrences[i] = Vector3.Distance(items[i].transform.position, ragdoll.position);
                if (itemDistanceRefrences[i] < minDistance)
                {
                    minDistance = itemDistanceRefrences[i];
                    closestItem = items[i];
                    indexMin = i;
                }
            }
        }
    }

    public void pickupItem()
    {
        if (aim.transform.position.x > transform.position.x)
        {
            direction = 6f;
        }
        else
        {
            direction = -6f;
        }

        weaponIndex = closestItem.GetComponent<WeaponIndexHolder>().WeaponIndex;

        if (refrenceKeeper.weaponInventory.Count < 4)
        {
            refrenceKeeper.weaponInventory.Add(spawnWeapons.Weapons[weaponIndex].WeaponItem);
            refrenceKeeper.activeSlot = refrenceKeeper.weaponInventory.Count - 1;
        }
        else
        {
            int id = refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot].id;
            refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot] = spawnWeapons.Weapons[weaponIndex].WeaponItem;
            GetComponent<SpawnItem>().CmdSpawnDropped(closestItem, transform.position, id, direction, closestItem.GetComponent<BulletsLeft>().bullets); //Need to change this mannnn
        }

        Destroy(closestItem);
        itemDistanceRefrences.RemoveAt(indexMin);
        items.RemoveAt(indexMin);

        switchWeapon.Switch(weaponIndex);

        aimShoot.scale = spawnWeapons.Weapons[weaponIndex].WeaponItem.scale;
        aimShoot.position = spawnWeapons.Weapons[weaponIndex].WeaponItem.position;
        aimShoot.positionFlipped = spawnWeapons.Weapons[weaponIndex].WeaponItem.positionFlipped;
        shoot.fireRate = spawnWeapons.Weapons[weaponIndex].WeaponItem.fireRate;
        shoot.recoil = spawnWeapons.Weapons[weaponIndex].WeaponItem.recoil;
        shoot.impact = spawnWeapons.Weapons[weaponIndex].WeaponItem.impact;
        shoot.bulletsLeft[refrenceKeeper.activeSlot] = closestItem.GetComponent<BulletsLeft>().bullets;
        //Update Stats
        refrenceKeeper.inventoryCount++;
        refrenceKeeper.inventoryCount = Mathf.Clamp(refrenceKeeper.inventoryCount, 0, 4);

        updateUI.UpdateSlotsUI();
        updateUI.HighlightSlotOnPickup(refrenceKeeper.activeSlot);

        checkDistances();

        CmdSetWeapon(weaponIndex, closestItem);
    }

    [Command]
    public void CmdSetWeapon(int WI, GameObject weapon)
    {
        RpcSetWeapon(WI, weapon);
        if (!hasAuthority)
        {
            if (refrenceKeeper.weaponInventory.Count < 4)
            {
                refrenceKeeper.weaponInventory.Add(spawnWeapons.Weapons[weaponIndex].WeaponItem);
                refrenceKeeper.activeSlot = refrenceKeeper.weaponInventory.Count - 1;
            }
            else
            {
                refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot] = spawnWeapons.Weapons[weaponIndex].WeaponItem;
            }

            switchWeapon.Switch(WI);

            aimShoot.scale = spawnWeapons.Weapons[weaponIndex].WeaponItem.scale;
            aimShoot.position = spawnWeapons.Weapons[weaponIndex].WeaponItem.position;
            aimShoot.positionFlipped = spawnWeapons.Weapons[weaponIndex].WeaponItem.positionFlipped;
            shoot.fireRate = spawnWeapons.Weapons[weaponIndex].WeaponItem.fireRate;
            shoot.recoil = spawnWeapons.Weapons[weaponIndex].WeaponItem.recoil;
            shoot.impact = spawnWeapons.Weapons[weaponIndex].WeaponItem.impact;
            shoot.bulletsLeft[refrenceKeeper.activeSlot] = weapon.GetComponent<BulletsLeft>().bullets;

            refrenceKeeper.inventoryCount++;
            refrenceKeeper.inventoryCount = Mathf.Clamp(refrenceKeeper.inventoryCount, 0, 4);
        }
    }

    [ClientRpc]
    public void RpcSetWeapon(int WI, GameObject weapon)
    {
        if (!hasAuthority)
        {
            if (refrenceKeeper.weaponInventory.Count < 4)
            {
                refrenceKeeper.weaponInventory.Add(spawnWeapons.Weapons[weaponIndex].WeaponItem);
                refrenceKeeper.activeSlot = refrenceKeeper.weaponInventory.Count - 1;
            }
            else
            {
                refrenceKeeper.weaponInventory[refrenceKeeper.activeSlot] = spawnWeapons.Weapons[weaponIndex].WeaponItem;
            }

            switchWeapon.Switch(WI);

            aimShoot.scale = spawnWeapons.Weapons[weaponIndex].WeaponItem.scale;
            aimShoot.position = spawnWeapons.Weapons[weaponIndex].WeaponItem.position;
            aimShoot.positionFlipped = spawnWeapons.Weapons[weaponIndex].WeaponItem.positionFlipped;
            shoot.fireRate = spawnWeapons.Weapons[weaponIndex].WeaponItem.fireRate;
            shoot.recoil = spawnWeapons.Weapons[weaponIndex].WeaponItem.recoil;
            shoot.impact = spawnWeapons.Weapons[weaponIndex].WeaponItem.impact;
            shoot.bulletsLeft[refrenceKeeper.activeSlot] = weapon.GetComponent<BulletsLeft>().bullets;

            refrenceKeeper.inventoryCount++;
            refrenceKeeper.inventoryCount = Mathf.Clamp(refrenceKeeper.inventoryCount, 0, 4);
        }
    }
}
