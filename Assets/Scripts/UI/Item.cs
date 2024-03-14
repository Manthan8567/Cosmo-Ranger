using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Armor,
        Sword,
        HealthPotion
    }

    public static int GetCost (ItemType itemType)
    {
        switch (itemType)
        {
            default:
                case ItemType.Armor:  return 0;
                case ItemType.Sword:  return 1;
                case ItemType.HealthPotion: return 2;
        }
    }

    public static Sprite GetSprite (ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.Armor:        return GameAssets.i.armorSprite;
            case ItemType.Sword:        return GameAssets.i.swordSprite;
            case ItemType.HealthPotion: return GameAssets.i.healthPotionSprite;
        }
    }
}
