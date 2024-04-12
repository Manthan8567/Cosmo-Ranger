using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Equipment,
    Default,
    Diamonds
}
public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite UI_Display;
    public ItemType type;
    public int price;
    [TextArea(15, 30)]
    public string description;
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public ItemType type;
    public Item(ItemObject item)
    {
        Name = item.name;
        Id = item.Id;
        type = item.type;
    }
}
