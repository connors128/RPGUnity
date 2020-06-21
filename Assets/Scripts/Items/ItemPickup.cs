﻿﻿using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;	// Item to put in the inventory on pickup

    // When the player interacts with the item
    public override void Interact()
    {
        base.Interact();
        PickUp();	// Pick it up!
    }

    // Pick up the item
    void PickUp ()
    {
        Debug.Log("Picking up " + item.name);
        var wasPickedUp = Inventory.instance.Add(item);	// Add to inventory
        
        Debug.Log("wasPickedUp = " + wasPickedUp); // should be true
        
        // If successfully picked up
        if (!wasPickedUp) return;
        Destroy(gameObject);	// Destroy item from scene
        Debug.Log(item.name + " was picked up");
    }

}