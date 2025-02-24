using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Item", fileName = "New item")]

public class Items : ScriptableObject
{
    public string itemName;
    public string itemDes;
    public Sprite itemSprite;
    public bool isUseable;

    
}
