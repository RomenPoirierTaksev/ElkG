using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemPickup : MonoBehaviour
{
    public Item item;
    public string getName()
    {
        
        return item.name;
    }

    public Sprite getIcon()
    {
        return item.icon;
    }

    public Vector3 getHandPos()
    {
        return item.handPos;
    }

    public float getDamage()
    {
        try
        {
            Weapon newItem = (Weapon)item;
            return newItem.damage;
        }
        catch
        {
            return 0f;
        }
    }

    public float getRange()
    {
        try
        {
            Weapon newItem = (Weapon)item;
            return newItem.range;
        }
        catch
        {
            return 0f;
        }
       
    }
}
