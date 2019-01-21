using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RefrenceKeeperAI : NetworkBehaviour
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

    public UpdateUI updateUI;

    public bool one = true;
    public bool two;
    public bool three;
    public bool four;

    private bool updated = false;

    public override void OnStartAuthority()
    {
        if (hasAuthority)
        {
            updateUI = GameObject.Find("PlayerUI").GetComponent<UpdateUIRefrence>().updateUI;
            updateUI.refrenceKeeper = this;
            updateUI.OpenClose();
        }
    }

    void Update()
    {
        if (!updated && updateUI)
        {
            updateUI.HighlightSlot(activeSlot);
            updated = true;
        }
        if (weaponInventory.Count > 0)
        { //If these buttons are smashed multiple guns can be spawned, implement a check to see if child count is greater than three and delete anything higher than that.
            if (one)
            {
                activeSlot = 0;
                activeSlot = Mathf.Clamp(activeSlot, 0, weaponInventory.Count - 1);
                switchWeapon.Switch(weaponInventory[activeSlot].id);
                if (hasAuthority)
                {
                    updateUI.HighlightSlot(activeSlot);
                }
                one = false;
            }
            if (two)
            {
                activeSlot = 1;
                activeSlot = Mathf.Clamp(activeSlot, 0, weaponInventory.Count - 1);
                switchWeapon.Switch(weaponInventory[activeSlot].id);
                if (hasAuthority)
                {
                    updateUI.HighlightSlot(activeSlot);
                }
                two = false;
            }
            if (three)
            {
                activeSlot = 2;
                activeSlot = Mathf.Clamp(activeSlot, 0, weaponInventory.Count - 1);
                switchWeapon.Switch(weaponInventory[activeSlot].id);
                if (hasAuthority)
                {
                    updateUI.HighlightSlot(activeSlot);
                }
                three = false;
            }
            if (four)
            {
                activeSlot = 3;
                activeSlot = Mathf.Clamp(activeSlot, 0, weaponInventory.Count - 1);
                switchWeapon.Switch(weaponInventory[activeSlot].id);
                if (hasAuthority)
                {
                    updateUI.HighlightSlot(activeSlot);
                }
                four = false;
            }
        }
    }
}

