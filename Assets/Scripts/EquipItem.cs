﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    RaycastHit hit;
    public GameObject rightHand;
    public static bool itemEquiped = false;
    public static GameObject equipedItem;
    Inventory inventory;


    void Start()
    {
        GameEvents.instance.onToolPickUp += equipItem;
        inventory = gameObject.GetComponent<Inventory>();
    }

    private void equipItem()
    {
        Transform transform = Camera.main.GetComponent<Transform>();
        int layerMask = 1 << 10;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 6f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            if (inventory.getNumberItems() == inventory.maxInventorySize)
            {
                return;
            }

            if (hit.collider.tag.Equals("Equipable"))
            {
                equipedItem = hit.transform.gameObject;
                if (inventory.addItemToInventory(equipedItem))
                {
                    hit.rigidbody.velocity = Vector3.zero;
                    hit.transform.parent = rightHand.transform.parent;
                    hit.transform.localPosition = rightHand.transform.localPosition;
                    hit.transform.localRotation = Quaternion.Euler(hit.transform.gameObject.GetComponent<Weapon>().getHandPos());
                    hit.rigidbody.useGravity = false;
                    hit.transform.parent = rightHand.transform;
                    hit.transform.localPosition = hit.transform.localPosition + Vector3.up * 0.07120773f + Vector3.forward * 0.05999654f;
                    hit.collider.enabled = false;
                    itemEquiped = true;

                }
                //Debug.Log("Equipped");
            }
            else
            {
                unequipItem();
            }
        }
        else
        {
            unequipItem();
        }
    }

    private void unequipItem()
    {
        equipedItem = inventory.removeItemFromInventory();
        if (equipedItem != null)
        {
            itemEquiped = false;
            equipedItem.transform.parent = null;
            equipedItem.GetComponent<Rigidbody>().useGravity = true;
            equipedItem.GetComponent<Collider>().enabled = true;
            equipedItem.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 70, 300));
            Debug.Log("Dropped");
        }

    }
}

    

