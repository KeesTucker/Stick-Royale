using Mirror;
using UnityEngine;

public class SpawnItem : NetworkBehaviour {

    public GameObject switchedWeapon;
    public SpawnWeapons spawnWeapons;
    public GameObject items;
    public LocaliseTransform localiseTransform;

    // Use this for initialization
    void Start () {
        spawnWeapons = GameObject.Find("Items").GetComponent<SpawnWeapons>();
        items = GameObject.Find("Items");
        localiseTransform = GameObject.Find("Items").GetComponent<LocaliseTransform>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    [Command]
    public void CmdSpawnDropped(GameObject ItemPrefab, Vector3 position, int id, float direction, int bulletsLeft)
    {
        switchedWeapon = Instantiate(ItemPrefab, position, Quaternion.identity);
        switchedWeapon.GetComponent<Pickup>().WeaponIndex = id;
        Destroy(switchedWeapon.transform.GetChild(0).gameObject);
        GameObject WeaponModel = Instantiate(
            spawnWeapons.Weapons[id].WeaponItem.itemModel,
            switchedWeapon.transform.position,
            switchedWeapon.transform.rotation);
        WeaponModel.transform.SetParent(switchedWeapon.transform);
        WeaponModel.transform.localPosition = new Vector3(0, 0, 0);
        switchedWeapon.transform.position = switchedWeapon.transform.position + new Vector3(direction, 1, 0);
        WeaponModel.gameObject.layer = 11;
        switchedWeapon.GetComponent<BulletsLeft>().bullets = bulletsLeft;
        for (int z = 0; z < WeaponModel.transform.childCount; z++)
        {
            WeaponModel.transform.GetChild(z).gameObject.layer = 11;
        }
        switchedWeapon.GetComponent<Pickup>().switchSpawned = true;
        switchedWeapon.GetComponent<Pickup>().deactivated = false;
        switchedWeapon.transform.SetParent(items.transform);
        localiseTransform.setTransformItem(WeaponModel, id);
        NetworkServer.Spawn(switchedWeapon);
    }
}
