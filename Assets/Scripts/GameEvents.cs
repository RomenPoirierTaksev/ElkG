using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action onItemPickUp;
    public void itemPickUp()
    {
        if(onItemPickUp != null)
        {
            onItemPickUp();
        }
    }

    public event Action onItemDrop;
    public void itemDrop()
    {
        if (onItemDrop != null)
        {
            onItemDrop();
        }
    }

    public event Action onbackpackExit;

    public void backpackExit()
    {
        if(onbackpackExit != null)
        {
            onbackpackExit();
        }
    }



    /**
    public event Action onDamageTaken;

    public void TakeDamage()
    {
        if(onDamageTaken != null)
        {
            onDamageTaken();
        }
    }
    **/

}
