﻿using UnityEngine;
 using UnityEngine.UI;
 

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour
{
    static GameObject iconGO;
    static GameObject removeButton;	// Reference to the remove button

    Sprite newSprite;
    public Item item;  // Current item in the slot

    public bool hasbeenpushed = false;
    // Add item to the slot
    public void AddItem (Item addedItem)
    {
        item = addedItem;
        iconGO = transform.GetChild(2).gameObject;
        removeButton = transform.GetChild(1).gameObject;

        

        iconGO.GetComponent<Image>().enabled = true;
        iconGO.GetComponent<Image>().sprite = addedItem.icon;
        removeButton.GetComponent<Button>().interactable = true;

        //Debug.Log("Added " + addedItem.name + " and changed slot info");
    }

    // Clear the slot
    public void ClearSlot ()
    {
        iconGO.GetComponent<Image>().sprite = null;
        iconGO.GetComponent<Image>().enabled = false;
        removeButton.GetComponent<Button>().interactable = false;
    }

    // Called when the remove button is pressed
    public void OnRemoveButton ()
    {
        hasbeenpushed = true;
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