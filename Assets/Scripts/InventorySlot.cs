using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot:MonoBehaviour
{
    Image item;
    private void Start()
    {
        item = gameObject.GetComponent<Image>();
    }

    public void setItem(Sprite itemSprite)
    {
        item.sprite = itemSprite;
        item.color = new Color(255, 255, 255, 255);
    }

    public void removeItem()
    {
        item.sprite = null;
        item.color = new Color(255, 255, 255, 0);
    }

    public string getSpriteName()
    {
        if(item.sprite != null)
        {
            return item.sprite.name;
        }
        return "";
    }
}
