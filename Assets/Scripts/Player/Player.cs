using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IShopCustomer
{


     private PlayerInventory inventory;
    private int Diamonds;
    
    public static Player Instance {  get; private set; } 
    public void BoughtItem(Item.ItemType itemType)
    {
        
        Debug.Log("Bought item: " + itemType);
    }
    /**/
    public bool TrySpendDiamondAmount(int amount)
    {
        return true;
    }
}
