﻿using UnityEngine;

/* This object updates the inventory UI. */

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;	// The parent object of all the items
    public GameObject inventoryUI;	// The entire UI

    Inventory inventory;	// Our current inventory

    InventorySlot[] slots;	// List of all the slots
    int i = 0;

    void Start () {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;	// Subscribe to the onItemChanged callback

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

    // Update the inventory UI by:
    //		- Adding items
    //		- Clearing empty slots
    // This is called using a delegate on the Inventory.
    public void UpdateUI()
    {
        for (i = 0; i < slots.Length; i++) //slots.length = 20
        {
            if (i < inventory.items.Count) // If there is an item to add
            {
                Debug.Log(inventory.items.Count + " in array");
                Debug.Log("Adding slot in: " + i); //i is always 0, does not work
                slots[i].AddItem(inventory.items[i]); // Add it
                Debug.Log("after item should have been added to UI");
            }
            else
            {
                // Otherwise clear the slot
                Debug.Log("slot was cleared"); //not called
                //slots[i].ClearSlot();
            }
            i++;
        }
    }
}