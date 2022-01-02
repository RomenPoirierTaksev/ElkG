using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    Dictionary<int, GameObject> inventory = new Dictionary<int, GameObject>();
    public int maxInventorySize = 10;
    public bool isFull = false;
    public static int currentIndex = 0;

    void Start()
    {
        currentIndex = Mathf.Clamp(currentIndex, 0, inventory.Count);
    }
    public void addItemToInventory(GameObject item)
    {
        if(inventory.Count < maxInventorySize)
        {
            inventory.Add(inventory.Count, item);
            currentIndex = inventory.Count - 1;
            activateInv();
            
        }
    }

    public GameObject removeItemFromInventory()
    {
        inventory.Remove(currentIndex, out GameObject removedItem);
        currentIndex--;
        isFull = false;
        activateInv();
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
        
        Vector2 mouseWheel = Input.mouseScrollDelta;
        if (mouseWheel != Vector2.zero)
        {
            if (mouseWheel.y < 0)
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = inventory.Count - 1;
                }
            }
            else
            {
                currentIndex++;
                if(currentIndex > inventory.Count -1 )
                {
                    currentIndex = 0;
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
                currentIndex = n - 1;
                
                activateInv();

            }
        }
         
    }

}
