﻿using UnityEngine;
 using UnityEngine.UIElements;

 /* The base item class. All items should derive from this. */

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    public new string name;	// Name of the item
    public Sprite icon;				// Item icon
    public bool isDefaultItem;      // Is the item default wear?

    // Called when the item is pressed in the inventory
    public virtual void Use ()
    {
        // Use the item
        // Something might happen

        Debug.Log("Using Item " + name);
    }

    public void RemoveFromInventory ()
    {
        Inventory.instance.Remove(this);
    }
	
}