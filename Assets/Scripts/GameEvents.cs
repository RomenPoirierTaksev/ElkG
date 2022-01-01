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

    public event Action onToolPickUp;
    public void ToolPickUp()
    {
        if(onToolPickUp != null)
        {
            onToolPickUp();
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
