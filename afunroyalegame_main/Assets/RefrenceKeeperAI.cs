using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrenceKeeperAI : MonoBehaviour
{

    public GameObject WeaponHand;

    public List<GameObject> WeaponRefrences = new List<GameObject>();

    public List<float> itemDistanceRefrences = new List<float>();

    public List<Item> weaponInventory = new List<Item>();

    public List<Item> itemInventory = new List<Item>();

    public int itemCount;

    public int inventoryCount;

    public int activeSlot = 0;

    public SwitchWeaponAI switchWeapon;

    public bool one = true;
    public bool two;
    public bool three;
    public bool four;

    void Update()
    {
        if (weaponInventory.Count > 0)
        { //If these buttons are smashed multiple guns can be spawned, implement a check to see if child count is greater than three and delete anything higher than that.
            if (one)
            {
                activeSlot = 0;
                activeSlot = Mathf.Clamp(activeSlot, 0, weaponInventory.Count - 1);
                switchWeapon.Switch(weaponInventory[activeSlot].id);
                one = false;
            }
            if (two)
            {
                activeSlot = 1;
                activeSlot = Mathf.Clamp(activeSlot, 0, weaponInventory.Count - 1);
                switchWeapon.Switch(weaponInventory[activeSlot].id);
                two = false;
            }
            if (three)
            {
                activeSlot = 2;
                activeSlot = Mathf.Clamp(activeSlot, 0, weaponInventory.Count - 1);
                switchWeapon.Switch(weaponInventory[activeSlot].id);
                three = false;
            }
            if (four)
            {
                activeSlot = 3;
                activeSlot = Mathf.Clamp(activeSlot, 0, weaponInventory.Count - 1);
                switchWeapon.Switch(weaponInventory[activeSlot].id);
                four = false;
            }
        }
    }
}

