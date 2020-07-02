﻿using UnityEditor.Build;
 using UnityEngine;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;	// The parent object of all the items
    public GameObject inventoryUI;	// The entire UI

    Inventory inventory;	// The current inventory
    public static InventorySlot[] slots;	// List of all the UIslots
    
    static int atSlot = 0;
   void Start () {
        inventory = Inventory.instance;
        inventory.onItemAddCallback += AddToUI;	// Subscribe to the onItemChanged callback
        inventory.onItemRemoveCallback += RemoveFromUI;
        // Populate our slots array
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }
	
    void Update () {
        // Check to see if we should open/close the inventory
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void AddToUI()
    {
        slots[atSlot].AddItem(inventory.items[atSlot]);
        atSlot++;
    }

    void RemoveFromUI()
    {
        int removeSlot = Inventory.removingSlot;
        Debug.Log(removeSlot);
        slots[removeSlot].ClearSlot();
        slots[removeSlot].item = null;
        atSlot--;
        for (; removeSlot < inventory.items.Count; removeSlot++)
        {
            slots[removeSlot].AddItem(slots[removeSlot + 1].item);
            if(slots[removeSlot].item != null)
            {
                slots[removeSlot].ClearSlot();
                slots[removeSlot].item = null;
                break;
            }
            //slots[removeSlot].item = inventory.items[removeSlot+1];
        }
        slots[removeSlot+1].ClearSlot();
    }
}