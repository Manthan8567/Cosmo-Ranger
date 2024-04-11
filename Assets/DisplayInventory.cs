using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    public GameObject inventoryPanel;
    private bool isInventoryVisible = true;

    private void Start()
    {

        CreateDisplay();
        
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Is working");
            ToggleInventoryVisibility();
        }

        UpdateDisplay();

    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            itemsDisplayed.Add(inventory.Container[i], obj);
        }
    }

    public void UpdateDisplay()
    {
        // Create a list to keep track of slots to delete
        List<InventorySlot> slotsToDelete = new List<InventorySlot>();

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i].amount <= 0)
            {
                // If the amount is zero or negative, add the slot to the list of slots to delete
                slotsToDelete.Add(inventory.Container[i]);
            }
            else
            {
                if (itemsDisplayed.ContainsKey(inventory.Container[i]))
                {
                    // Update the text of the visual object if it already exists
                    itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                }
                else
                {
                    // Instantiate a new visual object if it doesn't exist
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    itemsDisplayed.Add(inventory.Container[i], obj);
                }
            }
        }

        // Remove the visual objects associated with slots to delete
        foreach (var slot in slotsToDelete)
        {
            if (itemsDisplayed.ContainsKey(slot))
            {
                Destroy(itemsDisplayed[slot]);
                itemsDisplayed.Remove(slot);
            }
            // Remove the slot from the inventory
            inventory.Container.Remove(slot);
        }
    }


    public void ToggleInventoryVisibility()
    {
        if (isInventoryVisible)
        {
            OpenInventory();
            isInventoryVisible = false;
        }
            
        if (!isInventoryVisible)
        {
            CloseInventory();
            isInventoryVisible = true;
        }
            
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        
        Time.timeScale = 0;
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        
        Time.timeScale = 1;
    }

}

