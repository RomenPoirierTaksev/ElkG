using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    public static Backpack instance;
    Dictionary<int, GameObject> backpack = new Dictionary<int, GameObject>();
    public bool backpackOpen = false;
    public int maxInventorySize = 6;
    GameObject bp;
    public Canvas inventoryUI;
    public GameObject currentlyEquiped;
    Transform clickedButton;
    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        try
        {
            bp = inventoryUI.transform.GetChild(3).gameObject;
            
        }
        catch { }

        
    }

    public bool addItemToInventory(GameObject item)
    {
        if (backpack.Count < maxInventorySize)
        {
            if (backpack.TryAdd(backpack.Count, item))
            {
                if (currentlyEquiped != null) currentlyEquiped = item;
                //print(backpack.Count);
                updateUI(item);
                return true;
            }
        }
        return false;
    }

    public bool removeItemFromInventory(out GameObject returnedItem)
    {
        backpack.Remove(0, out GameObject removedItem);
        returnedItem = removedItem;
        updateUI();
        return returnedItem != null;
    }

    public void itemClick(Button button)
    {

        clickedButton = button.transform.GetChild(0).transform;
    }

    void updateUI()
    {
        //inventoryUI.transform.GetChild(1).GetChild(currentlySelected + 1).GetChild(0).GetComponent<InventorySlot>().removeItem();
    }

    void updateUI(GameObject item)
    {
        try
        {
            
            inventoryUI.transform.GetChild(3).GetChild(backpack.Count - 1).GetComponentInChildren<InventorySlot>().setItem(item.GetComponent<itemPickup>().getIcon());
        }
        catch
        {

        }

    }
    // Update is called once per frame
    void Update()
    {
        if(clickedButton != null)
        {
            clickedButton.position = Input.mousePosition;
        }


        if (Input.GetKeyDown("i") && bp != null)
        {
            backpackOpen = !backpackOpen;
            bp.transform.localScale = backpackOpen ? Vector3.one : Vector3.zero;
            Cursor.lockState = backpackOpen ? CursorLockMode.None : CursorLockMode.Locked;
            if(clickedButton != null)
            {
                clickedButton.localPosition = Vector3.zero;
                clickedButton = null;
            }

        }
    }
}
