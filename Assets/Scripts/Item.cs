using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Rarity
{
    Common,
    Rare, 
    Epic,
    Legendary
}

[System.Serializable]
public class Item 
{
    public string Item_ID;

    public string DisplayName;

    public Sprite Item_Sprite;

    public Rarity Rarity;

   
}
