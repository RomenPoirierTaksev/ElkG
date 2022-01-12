using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    public GameObject rightHand;
    GameObject currentlyEquipedItem;
    Inventory inventory;
    public GameObject viewPos;

    /**
     * THIS ENTIRE CLASS IS CURRENTLY BEING REFACTORED IM FIGURING IT OUT
     * 
     */

    void Start()
    {
        inventory = gameObject.GetComponent<Inventory>();
        GameEvents.instance.onItemDrop += unequipItem;
    }

    
    public bool equipItem(GameObject equipedItem)
    {
        if (inventory.getNumberItems() == inventory.maxInventorySize) return false;
        if (Backpack.instance.addItemToInventory(equipedItem))
        {
            if (!setCurrentlyEquipedItem()) return false;
            Rigidbody itemRb = currentlyEquipedItem.GetComponent<Rigidbody>();
            itemRb.velocity = Vector3.zero;
            currentlyEquipedItem.transform.parent = rightHand.transform.parent;
            currentlyEquipedItem.transform.localPosition = rightHand.transform.localPosition;
            currentlyEquipedItem.transform.localRotation = Quaternion.Euler(currentlyEquipedItem.GetComponent<itemPickup>().getHandPos());
            itemRb.useGravity = false;
            currentlyEquipedItem.transform.parent = rightHand.transform;
            currentlyEquipedItem.transform.localPosition = currentlyEquipedItem.transform.localPosition + Vector3.up * 0.07120773f + Vector3.forward * 0.05999654f;
            currentlyEquipedItem.GetComponent<Collider>().enabled = false;
            return true;
        }
        return false;
    }

    bool setCurrentlyEquipedItem()
    {
        if (Backpack.instance.getCurrentEquiped(out GameObject eq))
        { 
            currentlyEquipedItem = eq;
            return true;
        }
        return false;
    }

    void unequipItem()
    {
        if (!setCurrentlyEquipedItem()) return;
        currentlyEquipedItem.transform.parent = null;
        currentlyEquipedItem.GetComponent<Rigidbody>().useGravity = true;
        currentlyEquipedItem.GetComponent<Collider>().enabled = true;
        currentlyEquipedItem.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 70, 300));

    }
}

    

