﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton

    public static Inventory instance;  //only one can exist

    void Awake ()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    // Callback which is triggered when
    // an item gets added/removed.
    public delegate void OnItemChanged( );
    public OnItemChanged onItemAddCallback;
    public OnItemChanged onItemRemoveCallback;
    public int space = 20;	// Amount of slots in inventory
    public static int removingSlot;

    // Current list of items in inventory
    public List<Item> items = new List<Item>();

    // Add a new item. If there is enough room we
    // return true. Else we return false.
    public bool Add (Item item)
    {
        // Don't do anything if it's a default item
        if (!item.isDefaultItem)
        {
            // Check if out of space
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }
            
            items.Add(item);	// Add item to list (built-in function)
            onItemAddCallback?.Invoke(); //updates UI
        }

        return true;
    }

    // Remove an item
    public void Remove (Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (InventoryUI.slots[i].hasbeenpushed) 
            {
                removingSlot = i;
                InventoryUI.slots[i].hasbeenpushed = false;
                break;
            }
        }
        // Trigger callback
        items.Remove(item);		// Remove item from list
        
        onItemRemoveCallback?.Invoke();


    }

}