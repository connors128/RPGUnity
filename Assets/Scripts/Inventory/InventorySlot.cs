﻿using UnityEngine;
 using UnityEngine.UIElements;
 using UnityEngine.UIElements;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour
{
    public Sprite icon;
    public Button removeButton;	// Reference to the remove button
    private Sprite newSprite;
    public Item item;  // Current item in the slot

    // Add item to the slot
    public void AddItem (Item addedItem)// Item newItem not set to an instance of an object
    {
        item = addedItem;
        newSprite = icon = item.icon;

        var childImage = GetComponentInChildren<Image>();
        childImage.image = item.icon.texture;
        newSprite = icon;
        
        //sprite = newSprite;
        //icon = sprite;
        //sprite.texture.Apply();
        
        //newSprite = item.icon;
        //item.icon = newSprite;
        Debug.Log("Adding " + item.name);
        
        
        Debug.Log("changed slot info");
    }

    // Clear the slot
    public void ClearSlot ()
    {
        var image = GetComponent<Image>();
        item = null;

        //image.sprite = null;
        image.visible = false;
        removeButton.visible = false;
    }

    // Called when the remove button is pressed
    public void OnRemoveButton ()
    {
        Inventory.instance.Remove(item);
    }

    // Called when the item is pressed
    public void UseItem ()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}