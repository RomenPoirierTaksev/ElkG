using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    Dictionary<int, GameObject> inventory = new Dictionary<int, GameObject>();
    public int maxInventorySize = 10;
    public bool isFull = false;
    int currentlySelected = 0;
    public Canvas inventoryUI;

    public bool addItemToInventory(GameObject item)
    {
        if(inventory.Count < maxInventorySize)
        {
            if(inventory.TryAdd(currentlySelected, item))
            {
                updateUI(item);
                activateInv();
                return true;
            }
        }
        return false;
    }

    void updateUI()
    {
        inventoryUI.transform.GetChild(1).GetChild(currentlySelected + 1).GetChild(0).GetComponent<InventorySlot>().removeItem();
    }

    void updateUI(GameObject item)
    {
        try
        {
            inventoryUI.transform.GetChild(1).GetChild(currentlySelected + 1).GetChild(0).GetComponent<InventorySlot>().setItem(item.GetComponent<Weapon>().getSprite());
        }
        catch
        {

        }

    }

    public bool removeItemFromInventory(out GameObject returnedItem)
    {
        //inventory.TryGetValue(currentlySelected, out GameObject item);
        //returnedItem = item;
        inventory.Remove(currentlySelected, out GameObject removedItem);
        returnedItem = removedItem;
        updateUI();
        isFull = false;
        activateInv();
        return returnedItem != null;
    }

    void activateInv()
    {
        print(currentlySelected + " " + inventory.Count);
        for (int i = 0; i < maxInventorySize; i++)
        {
            inventory.TryGetValue(i, out GameObject item);
            if(item != null)
            {
                if (i == currentlySelected)
                {
                    item.SetActive(true);
                }
                else
                {
                    item.SetActive(false);
                }
            }
        }
        try
        {
            Transform slotSelector = inventoryUI.transform.GetChild(1).GetChild(0);
            slotSelector.position = inventoryUI.transform.GetChild(1).GetChild(currentlySelected + 1).position;
        }
        catch{}
    }

    public int getNumberItems()
    {
        return inventory.Count;
    }

    void Update()
    {
        
        Vector2 mouseWheel = Input.mouseScrollDelta;
        if (mouseWheel != Vector2.zero)
        {
            if (mouseWheel.y < 0)
            {
                currentlySelected--;
                if (currentlySelected < 0)
                {
                    currentlySelected = maxInventorySize - 1;
                }
            }
            else
            {
                currentlySelected++;
                if(currentlySelected > maxInventorySize -1 )
                {
                    currentlySelected = 0;
                }
            }
            
            activateInv();
        }
        if (Input.anyKeyDown)
        {
            string keyPressed = Input.inputString;
            int n;
            bool r = int.TryParse(keyPressed, out n);
            if (r)
            {
                if(n == 0)
                {
                    return;
                }
                currentlySelected = n - 1;
                
                activateInv();

            }
        }
         
    }

}
