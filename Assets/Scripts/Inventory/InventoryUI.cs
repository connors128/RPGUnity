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
        
        slots[removeSlot].ClearSlot();
        slots[removeSlot].item = null;

        for(atSlot = 0; atSlot < inventory.items.Count; atSlot++)
        {
            slots[atSlot].AddItem(inventory.items[atSlot]);
        }
    }
}