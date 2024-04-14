using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventorySlotPrefab;
    public InventoryObject inventory;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();
    public GameObject inventoryPanel;
    private bool isInventoryVisible = true;

    private void Awake()
    {
        CreateDisplay();

        // Subscribe to the InventoryChanged event
        inventory.InventoryChanged += UpdateDisplay;

    }
    private void OnDestroy()
    {
        // Unsubscribe from the InventoryChanged event when the object is destroyed
        inventory.InventoryChanged -= UpdateDisplay;
    }
    
    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.items.Count; i++)
        {
            InventorySlot slotItem = inventory.Container.items[i];
            var obj = Instantiate(inventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
            var itemData = inventory.database.GetItem[slotItem.item.Id];
            if (itemData != null)
            {
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = itemData.UI_Display;
            }
            else
            {
                Debug.LogError("Item data not found for item ID: " + slotItem.item.Id);
            }
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slotItem.amount.ToString("n0");
            itemsDisplayed.Add(slotItem, obj);
        }
    }

    public void UpdateDisplay()
    {
        // Create a list to keep track of slots to delete
        List<InventorySlot> slotsToDelete = new List<InventorySlot>();

        for (int i = 0; i < inventory.Container.items.Count; i++)
        {
            InventorySlot slotItem = inventory.Container.items[i];
            if (slotItem.amount <= 0)
            {
                // If the amount is zero or negative, add the slot to the list of slots to delete
                slotsToDelete.Add(slotItem);
            }
            else
            {
                if (itemsDisplayed.ContainsKey(slotItem))
                {
                    // Update the text of the visual object if it already exists
                    itemsDisplayed[slotItem].GetComponentInChildren<TextMeshProUGUI>().text = slotItem.amount.ToString("n0");
                }
                else
                {
                    // Instantiate a new visual object if it doesn't exist
                    var obj = Instantiate(inventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);
                    var itemData = inventory.database.GetItem[slotItem.item.Id];
                    if (itemData != null)
                    {
                        obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = itemData.UI_Display;
                    }
                    else
                    {
                        Debug.LogError("Item data not found for item ID: " + slotItem.item.Id);
                    }
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = slotItem.amount.ToString("n0");
                    itemsDisplayed.Add(slotItem, obj);
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
            inventory.Container.items.Remove(slot);
        }
    }

}

