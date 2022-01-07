using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    public GameObject rightHand;
    public static bool itemEquiped = false;
    public static GameObject equipedItem;
    Inventory inventory;
    public GameObject viewPos;


    void Start()
    {
        inventory = gameObject.GetComponent<Inventory>();
    }
    public bool equipItem(GameObject equipedItem)
    {

        if (inventory.getNumberItems() == inventory.maxInventorySize)
        {
            return false ;
        }

        if (equipedItem != null && Backpack.instance.addItemToInventory(equipedItem) && Backpack.instance.currentlyEquiped == null)
        {
            Rigidbody itemRb = equipedItem.GetComponent<Rigidbody>();
            itemRb.velocity = Vector3.zero;
            equipedItem.transform.parent = rightHand.transform.parent;
            equipedItem.transform.localPosition = rightHand.transform.localPosition;
            equipedItem.transform.localRotation = Quaternion.Euler(equipedItem.GetComponent<itemPickup>().getHandPos());
            itemRb.useGravity = false;
            equipedItem.transform.parent = rightHand.transform;
            equipedItem.transform.localPosition = equipedItem.transform.localPosition + Vector3.up * 0.07120773f + Vector3.forward * 0.05999654f;
            equipedItem.GetComponent<Collider>().enabled = false;
            itemEquiped = true;
            return true;
        }
        return false;
    }

    public void unequipItem()
    {
        
        if (inventory.removeItemFromInventory(out GameObject equipedItem))
        {
            itemEquiped = false;
            equipedItem.transform.parent = null;
            equipedItem.GetComponent<Rigidbody>().useGravity = true;
            equipedItem.GetComponent<Collider>().enabled = true;
            equipedItem.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 70, 300));
        }

    }
}

    

