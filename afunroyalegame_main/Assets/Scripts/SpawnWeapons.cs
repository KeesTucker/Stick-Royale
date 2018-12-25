using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnWeapons : NetworkBehaviour {

    public GameObject WeaponItemPrefab;

    public int spawnNumber = 20;

    public int WeaponIndex;

    public Transform ItemsParent;

    public MeshCollider[] colliders;

    public Renderer[] renderers;

    public Object[] info;

    public Material WeaponMat;

    public GameObject WeaponModel;

    public List<Vector3> WeaponSpawnPoints = new List<Vector3>();
    
    ServerRefrenceKeeper refrenceKeeper;
    // Use this for initialization

    public List<WeaponList> Weapons = new List<WeaponList>();

    void Start()
    {
        refrenceKeeper = GameObject.Find("Server Refrences").GetComponent<ServerRefrenceKeeper>();
        info = Resources.LoadAll("Items", typeof(Item));
        foreach (Object fileInfo in info)
        {
            Item item = (Item)fileInfo;
            Weapons.Add(new WeaponList { WeaponItem = item, WeaponIndex = item.id });
        }
        refrenceKeeper.numOfWeapons = Weapons.Count;
        Weapons.Sort();
        if (isServer)
        {
            //Spawn Code Next
            for (int i = 0; i < spawnNumber; i++)
            {
                WeaponSpawnPoints.Add(new Vector3((i * 20), 0, 0)); //position code is just temporary for debugging
                WeaponIndex = Random.Range(0, refrenceKeeper.numOfWeapons);
                GameObject WeaponItem = Instantiate(
                    WeaponItemPrefab,
                    WeaponSpawnPoints[i],
                    transform.rotation);
                WeaponItem.GetComponent<Pickup>().WeaponIndex = WeaponIndex;
                WeaponItem.transform.SetParent(ItemsParent);
                WeaponItem.transform.localScale = new Vector3(1, 1, 1);
                WeaponItem.gameObject.layer = 11;
                WeaponItem.GetComponent<BulletsLeft>().bullets = Weapons[WeaponIndex].WeaponItem.magazineSize;
                WeaponItem.GetComponent<SphereCollider>().center = Weapons[WeaponIndex].WeaponItem.ItemColliderPos;
                WeaponItem.GetComponent<SphereCollider>().radius = 3f;
                NetworkServer.Spawn(WeaponItem);
                WeaponModel = Instantiate(
                    Weapons[WeaponIndex].WeaponItem.itemModel,
                    WeaponItem.transform.position,
                    transform.rotation);
                WeaponModel.gameObject.layer = 11;
                for (int z = 0; z < WeaponModel.transform.childCount; z++)
                {
                    WeaponModel.transform.GetChild(z).gameObject.layer = 11;
                }
                WeaponModel.transform.SetParent(WeaponItem.transform);
                colliders = WeaponModel.transform.GetComponentsInChildren<MeshCollider>();
                /*foreach (MeshCollider collider in colliders)
                {
                    collider.convex = true;
                }*/
                renderers = WeaponModel.transform.GetComponentsInChildren<Renderer>();
                foreach (Renderer renderer in renderers)
                {
                    renderer.material = WeaponMat;
                }
                WeaponModel.transform.localPosition = Weapons[WeaponIndex].WeaponItem.spawnPosition;
                WeaponModel.transform.localEulerAngles = Weapons[WeaponIndex].WeaponItem.spawnRotation;
                WeaponModel.transform.localScale = Weapons[WeaponIndex].WeaponItem.spawnScale;
                NetworkServer.Spawn(WeaponItem);
            }
        }
    }
}
