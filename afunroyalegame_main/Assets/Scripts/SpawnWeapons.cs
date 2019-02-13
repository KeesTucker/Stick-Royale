using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Collections;

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

    public GameObject[] spawns;
    
    // Use this for initialization

    public List<WeaponList> Weapons = new List<WeaponList>();

    public bool done = false;

    void Start()
    {
        info = Resources.LoadAll("Items", typeof(Item));
        foreach (Object fileInfo in info)
        {
            Item item = (Item)fileInfo;
            Weapons.Add(new WeaponList { WeaponItem = item, WeaponIndex = item.id });
        }
        Weapons.Sort();
        done = true;
        
        if (isServer)
        {
            StartCoroutine("Wait");
        }
    }

    IEnumerator Wait()
    {
        while (!GameObject.Find("LocalPlayer"))
        {
            yield return null;
        }
        SpawnRocketAI spawnRocket = GameObject.Find("LocalPlayer").GetComponent<SpawnRocketAI>();
        while (!spawnRocket.ready)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        spawns = GameObject.FindGameObjectsWithTag("WeaponSpawn");
        for (int i = 0; i < spawns.Length; i++)
        {
            WeaponIndex = Random.Range(0, Weapons.Count - 2);
            GameObject WeaponItem = Instantiate(
                WeaponItemPrefab,
                spawns[i].transform.position,
                transform.rotation);
            WeaponItem.GetComponent<WeaponIndexHolder>().WeaponIndex = WeaponIndex;
            WeaponItem.transform.SetParent(ItemsParent);
            WeaponItem.GetComponent<BulletsLeft>().bullets = Weapons[WeaponIndex].WeaponItem.magazineSize;
            NetworkServer.Spawn(WeaponItem);
        }
    }
}
