using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;
using System;
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{

    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;

    public event Action InventoryChanged;



    public void AddItem(Item _item, int _amount)
    {
        // Check if the item already exists in the inventory
        bool hasItem = false;
        for (int i = 0; i < Container.items.Count; i++)
        {
            if (Container.items[i].item.Id == _item.Id) 
            {
                // If the amount to add is negative, subtract the item instead
                if (_amount < 0)
                {
                    // Ensure we don't subtract more than what's available in the inventory
                    Container.items[i].AddAmount(-Mathf.Min(Container.items[i].amount, -_amount));
                }
                else
                {
                    // Add the item with the specified amount
                    Container.items[i].AddAmount(_amount);
                }
                hasItem = true;
                break;
            }
        }
        // If the item doesn't exist in the inventory, add it
        if (!hasItem)
        {
            // Ensure that when subtracting, we don't add the item with a negative amount
            if (_amount >= 0)
            {
                Container.items.Add(new InventorySlot(_item.Id, _item, _amount));
            }
        }

        // Call the event to notify subscribers that the inventory has changed
        InventoryChanged?.Invoke();
    }

    public int GetItemCount(Item _item)
    {
        int itemCount = 0;

        foreach (InventorySlot slot in Container.items)
        {
            if (slot.item == _item)
            {
                itemCount += slot.amount;
            }
        }

        return itemCount;
    }


    [ContextMenu("Save")]
    public void Save()
    {

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
        Debug.Log("Invetory has been saved");
    }

    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Container = (Inventory)formatter.Deserialize(stream);
            stream.Close();
            Debug.Log("Invetory has been loaded");
        }
        else
        {
            Save();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
        InventoryChanged?.Invoke();
    }

    public void DeleteSaveFile()
    {
        // Combine the persistent data path and the save path to get the full file path
        string filePath = Path.Combine(Application.persistentDataPath, savePath);

        // Check if the save file exists
        if (File.Exists(filePath))
        {
            // Delete the save file
            File.Delete(filePath);
            Debug.Log("Save file deleted: " + filePath);
        }
        else
        {
            Debug.LogWarning("Save file not found: " + filePath);
        }
    }
}


[System.Serializable]
public class Inventory
{
    public List<InventorySlot> items = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item;
    public int amount;
    public InventorySlot( int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
