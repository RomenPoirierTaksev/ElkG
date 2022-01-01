using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Dictionary<int, GameObject> inventory = new Dictionary<int, GameObject>();
    public int maxInventorySize = 10;
    public bool isFull = false;
    public static int currentIndex = 0;

    public void addItemToInventory(GameObject item)
    {
        if(inventory.Count < maxInventorySize)
        {
            inventory.Add(inventory.Count, item);
            currentIndex = inventory.Count - 1;
            activateInv();
            
        }
        
        foreach (KeyValuePair<int, GameObject> i in inventory)
        {
            Debug.Log(i);
        }
    }

    public GameObject removeItemFromInventory()
    {
        inventory.Remove(currentIndex);
        currentIndex--;
        inventory.TryGetValue(currentIndex, out GameObject removedItem);
        isFull = false;
        activateInv();
        foreach (KeyValuePair<int, GameObject> i in inventory)
        {
            Debug.Log(i);
        }
        return removedItem;
    }

    void activateInv()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventory.TryGetValue(i, out GameObject item);
            if (i == currentIndex)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }

        }
    }

    public int getNumberItems()
    {
        return inventory.Count;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            string keyPressed = Input.inputString;
            int n;
            bool r = int.TryParse(keyPressed, out n);
            if (r)
            {
                currentIndex = n - 1;
                
                activateInv();

            }
        }
    }

}
