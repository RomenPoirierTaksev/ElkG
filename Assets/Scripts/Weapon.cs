using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    string weaponName;
    float damage;
    float range;
    Vector3 handPos;
    
    void Start()
    {   
        weaponName = gameObject.name;
        if (weaponName.ToLower().Contains("sword"))
        {
            damage = 25f;
            range = 3f;
            handPos = new Vector3(gameObject.transform.localRotation.x - 65, 0, 85);
        }
        else
        {
            damage = 5f;
            range = 1f;
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

    public Vector3 getHandPos()
    {
        return handPos;
    }
}
