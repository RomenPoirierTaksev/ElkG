using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    string weaponType;
    float damage;
    float range;
    
    void Start()
    {   
        weaponType = gameObject.name;
        switch (weaponType)
        {
            case "Sword":
                damage = 25f;
                range = 3f;
                break;
        }
    }

    public float getDamage()
    {
        return damage;
    }

    public float getRange()
    {
        return range;
    }
}
