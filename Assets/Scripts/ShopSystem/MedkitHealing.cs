using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitHealing : MonoBehaviour
{
    [SerializeField] private InventoryObject inventory; // Reference to the InventoryObject
    [SerializeField] private Health playerHealth; // Reference to the Health component
    [SerializeField] private FoodObject medKitItem; // Reference to the medkit item
    [SerializeField] private ParticleSystem medkitEffect;

    private void Start()
    {
        // Find or reference the InputReader2 component
        InputReader2 inputReader = FindObjectOfType<InputReader2>();

        // Subscribe to the UseMedkitEvent
        if (inputReader != null)
        {
            inputReader.UseMedkitEvent += UseMedkit;
        }
    }

    private void OnDestroy()
    {
        InputReader2 inputReader = FindObjectOfType<InputReader2>();
        if (inputReader != null)
        {
            inputReader.UseMedkitEvent -= UseMedkit;
        }
    }

    private void UseMedkit()
    {
        TrySpendMedkitsAmount(1); // Change the amount as needed
    }
    public bool TrySpendMedkitsAmount(int amount)
    {
        // Get the medkit quantity from the inventory
        int medKitQuantity = GetMedkitQuantity();

        // Ensure that the medkit quantity is greater than or equal to the specified amount
        if (medKitQuantity >= amount)
        {
            // Deduct the specified amount of diamonds from the inventory
            // Use the AddItem method with a negative amount to subtract diamonds
            inventory.AddItem(GetMedKitItemObject(), -amount);
            Debug.Log("Is working");
            Debug.Log(playerHealth);
            if (playerHealth != null)
            {
                playerHealth.Heal(medKitItem.restoreHealthvalue); // Restore health using public method from Health class
                Debug.Log($"Health fully restored to: {playerHealth.CurrHealth}");

            }
            if (medkitEffect != null)
            {
                medkitEffect.Play();
                AudioManager.Singleton.PlaySoundEffect("LevelUp") ;
            }
            // Notify other classes that the diamond amount has changed
            //OnDiamondCollected?.Invoke(this, EventArgs.Empty);

            return true;
        }
        Debug.Log("Not enough medkits!");
        return false;
    }

    public Item GetMedKitItemObject()
    {
        foreach (InventorySlot slot in inventory.Container.items)
        {
            // Check if the item in the inventory slot represents diamonds
            if (slot.item.type == ItemType.Food)
            {
                return slot.item;
            }
        }

        // If no medkit item is found, return null or handle the case accordingly
        return null;
    }

    public int GetMedkitQuantity()
    {
        Item diamondItem = GetMedKitItemObject();

        if (diamondItem != null)
        {
            int diamondQuantity = 0;

            foreach (InventorySlot slot in inventory.Container.items)
            {
                if (slot.item == diamondItem)
                {
                    diamondQuantity += slot.amount;
                }
            }

            return diamondQuantity;
        }
        else
        {
            return 0; // Return 0 if no diamond item is found in the inventory
        }
    }
}
