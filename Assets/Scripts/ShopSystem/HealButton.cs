using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealButton : MonoBehaviour
{
    private Health playerHealth; // Reference to the Health component
    public FoodObject MedKitItem; // Reference to the item component

    private void Awake()
    {
        playerHealth = FindObjectOfType<Player>().GetComponent<Health>();
        Debug.Log(playerHealth);
    }

    public void Heal()
    {
        Debug.Log("Is working");
        Debug.Log(playerHealth);
        if (playerHealth != null)
        {
            playerHealth.Heal(MedKitItem.restoreHealthvalue); // Restore health using public method from Health class
            Debug.Log($"Health fully restored to: {playerHealth.CurrHealth}");

        }
    }
}
