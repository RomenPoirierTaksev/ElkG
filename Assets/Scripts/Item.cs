using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "AllItems/Item")]
public class Item : ScriptableObject
{
    new public string name;
    public Sprite icon;
    public Vector3 handPos;
}
