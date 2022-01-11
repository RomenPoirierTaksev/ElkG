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
    Transform previousButton;
    Transform clickedButton;
    int numOfClicks = 0;
    bool canDrop = false;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        GameEvents.instance.onbackpackExit += allowDrop;

        try
        {
            bp = inventoryUI.transform.GetChild(3).gameObject;
            
        }
        catch { }

        
    }

    void allowDrop()
    {
        canDrop = true;
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

    public void removeItemFromInventory()
    {
        if (clickedButton == null) return;
        bool s = backpack.Remove(int.Parse(clickedButton.name.Substring(clickedButton.name.Length - 1)));
        if (s)
        {
            updateUI();
            canDrop = false;
        }
    }

    public void itemClick(Button button)
    {
        previousButton = clickedButton;
        clickedButton = button.transform.GetChild(0).transform;
        modifyItemSlot();
        canDrop = false;
        //print("clicked" + button.name);
    }

    void updateUI()
    {
        try
        {
            print(inventoryUI.transform.GetChild(3).GetChild(0));
            inventoryUI.transform.GetChild(3).GetChild(0).GetComponentInChildren<InventorySlot>().removeItem();
        }
        catch { }
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

    void modifyItemSlot()
    {
        if (numOfClicks == 2)
        {
            if (previousButton != null && clickedButton != previousButton && previousButton.gameObject.GetComponentInChildren<InventorySlot>().getIcon() != null)
            {
                InventorySlot one = clickedButton.gameObject.GetComponentInChildren<InventorySlot>();
                InventorySlot two = previousButton.gameObject.GetComponentInChildren<InventorySlot>();
                int oldIndex = int.Parse(previousButton.name.Substring(previousButton.name.Length - 1));
                int newIndex = int.Parse(clickedButton.name.Substring(clickedButton.name.Length - 1));
                
                if (backpack.Remove(oldIndex, out GameObject v)) 
                { 
                    backpack.Add(newIndex, v);
                    one.setItem(two.getIcon());
                    two.removeItem();
                    two.transform.localPosition = Vector3.zero;
                }
            }
            else
            {
                if(canDrop) removeItemFromInventory();
            }
            numOfClicks = 0;
            clickedButton = null;
            previousButton = null;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if (backpackOpen && Input.GetMouseButtonDown(0))
        {
            if(numOfClicks >= 2)
            {
                clickedButton = null;
                previousButton = null;
                numOfClicks = 0;
            }
            else numOfClicks++;

        }

        if (clickedButton != null && clickedButton.gameObject.GetComponent<InventorySlot>().getIcon() != null)
        {
            Transform parent = clickedButton.parent;
            parent.SetAsFirstSibling();
            clickedButton.position = Input.mousePosition;
        }

        if (Input.GetKeyDown("i") && bp != null)
        {
            backpackOpen = !backpackOpen;
            bp.transform.localScale = backpackOpen ? Vector3.one : Vector3.zero;
            Cursor.lockState = backpackOpen ? CursorLockMode.None : CursorLockMode.Locked;
            if(clickedButton != null)
            {
                if(previousButton != null)
                {
                    previousButton.localPosition = Vector3.zero;
                    previousButton = null;
                }
                clickedButton.localPosition = Vector3.zero;
                clickedButton = null;
            }

        }
    }
}
