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
        previousButton = clickedButton;
        clickedButton = button.transform.GetChild(0).transform;
        modifyItemSlot();
        print("clicked" + button.name);
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

    void modifyItemSlot()
    {
        if (numOfClicks == 2)
        {
            if (previousButton != null && clickedButton != previousButton && previousButton.gameObject.GetComponentInChildren<InventorySlot>().getIcon() != null)
            {
                InventorySlot one = clickedButton.gameObject.GetComponentInChildren<InventorySlot>();
                InventorySlot two = previousButton.gameObject.GetComponentInChildren<InventorySlot>();
                one.setItem(two.getIcon());
                two.removeItem();
                two.transform.localPosition = Vector3.zero;
            }
            else
            {
                print("dropped item");
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
            //not finished yet
            //clickedButton.gameObject.GetComponent<Image>();
            clickedButton.position = Input.mousePosition + Vector3.forward * 0.5f;
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
