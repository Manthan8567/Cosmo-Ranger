using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI diamondText;
    // Start is called before the first frame update
    void Start()
    {
        diamondText = GetComponent<TextMeshProUGUI>();

        // Subscribe to the OnDiamondCollected event
        Player playerInventory = FindObjectOfType<Player>();
        if (playerInventory != null)
        {
            playerInventory.OnDiamondCollected += HandleDiamondCollected;
            // Update UI initially
            UpdateDiamondText(playerInventory);
        }
    }

    public void UpdateDiamondText(Player playerInventory)
    {
        diamondText.text = playerInventory.NumberOfDiamonds.ToString();
    }

    // Event handler for the DiamondCollected event
    void HandleDiamondCollected(object sender, EventArgs e)
    {
        // Cast sender to PlayerInventory if needed
        Player playerInventory = (Player)sender;
        UpdateDiamondText(playerInventory);
    }

    // Remember to unsubscribe from events when the object is destroyed
    private void OnDestroy()
    {
        Player playerInventory = FindObjectOfType<Player>();
        if (playerInventory != null)
        {
            playerInventory.OnDiamondCollected -= HandleDiamondCollected;
        }
    }
}
