using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IShopCustomer
{
     
    public static Player Instance {  get; private set; }

    public int NumberOfDiamonds { get; set; }

    public event EventHandler OnDiamondCollected;

    private InventoryUI inventoryUI;

     private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void DiamondCollected()
    {
        NumberOfDiamonds++;
        OnDiamondCollected?.Invoke(this, EventArgs.Empty);
    }

    // this method is where you can change the outfit of the player 
    public void BoughtItem(Item.ItemType itemType)
    {
        
        Debug.Log("Bought item: " + itemType);
    }
    /**/
    public bool TrySpendDiamondAmount(int amount)
    {
        // Ensure the inventory is not null before accessing it
        if (NumberOfDiamonds >= amount)
        {
            NumberOfDiamonds -= amount;
            // Notify other classes that the gold amount has changed
            OnDiamondCollected?.Invoke(this, EventArgs.Empty);

            return true;
        }

        return false;
    }
}
