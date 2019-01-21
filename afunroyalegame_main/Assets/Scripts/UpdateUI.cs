using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUI : MonoBehaviour {

    public RefrenceKeeperAI refrenceKeeper;

    public ServerRefrenceKeeper serverRefrenceKeeper;

    public Transform itemsParent;

    public SlotUI[] inventorySlots;

    public GameObject canvas;

    IEnumerator Start()
    {
        serverRefrenceKeeper = GameObject.Find("Server Refrences").GetComponent<ServerRefrenceKeeper>();
        inventorySlots = itemsParent.GetComponentsInChildren<SlotUI>();
        yield return new WaitForEndOfFrame();
        serverRefrenceKeeper.updateUI = gameObject.GetComponent<UpdateUI>();
    }

    public void UpdateSlotsUI()
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < refrenceKeeper.weaponInventory.Count)
            {
                inventorySlots[i].AddItem(refrenceKeeper.weaponInventory[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
            
        }
        
    }

    public void OpenClose()
    {
        if(canvas.activeSelf == false)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    public void HighlightSlot(int activeSlot)
    {
        if(refrenceKeeper.weaponInventory.Count > 0)
        {
            activeSlot = Mathf.Clamp(activeSlot, 0, refrenceKeeper.weaponInventory.Count - 1);
            for (int i = 0; i < 8; i++)
            {
                gameObject.transform.GetChild(0).GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
            gameObject.transform.GetChild(0).GetChild(activeSlot).GetChild(0).gameObject.SetActive(true);
        }
    }
    public void HighlightSlotOnPickup(int activeSlot)
    {
        activeSlot = Mathf.Clamp(activeSlot, 0, refrenceKeeper.weaponInventory.Count);
        for (int i = 0; i < 8; i++)
        {
            gameObject.transform.GetChild(0).GetChild(i).GetChild(0).gameObject.SetActive(false);
        }
        gameObject.transform.GetChild(0).GetChild(activeSlot).GetChild(0).gameObject.SetActive(true);
    }

}
