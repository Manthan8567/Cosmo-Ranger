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
        // Iterate backwards over the inventory items to avoid modifying the collection while iterating
        for (int i = inventory.Container.items.Count - 1; i >= 0; i--)
        {
            InventorySlot slotItem = inventory.Container.items[i];
            if (slotItem.amount <= 0)
            {
                // If the amount is zero or negative, remove the slot from the display and the container
                if (itemsDisplayed.ContainsKey(slotItem))
                {
                    Destroy(itemsDisplayed[slotItem]);
                    itemsDisplayed.Remove(slotItem);
                }
                inventory.Container.items.RemoveAt(i);
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
    }
}

