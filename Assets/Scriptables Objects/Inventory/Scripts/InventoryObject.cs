using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject,ISerializationCallbackReceiver
{
    public string savePath;
    private ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
#else
        database = Resources.Load<ItemDatabaseObject>("Database");
#endif
    }
    public void AddItem(ItemObject _item, int _amount)
{
    // Check if the item already exists in the inventory
    bool hasItem = false;
    for (int i = 0; i < Container.Count; i++)
    {
        if (Container[i].item == _item) 
        {
            // If the amount to add is negative, subtract the item instead
            if (_amount < 0)
            {
                // Ensure we don't subtract more than what's available in the inventory
                Container[i].AddAmount(-Mathf.Min(Container[i].amount, -_amount));
            }
            else
            {
                // Add the item with the specified amount
                Container[i].AddAmount(_amount);
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
            Container.Add(new InventorySlot(database.GetID[_item], _item, _amount));
        }
    }
}


    public void OnAfterDeserialize()
    {
        for(int i = 0;i < Container.Count; i++)
            Container[i].item = database.GetItem[Container[i].ID];
    }

    public void OnBeforeSerialize()
    {
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
        
    }

    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }
}
[System.Serializable]
public class InventorySlot
{
    public int ID;
    public ItemObject item;
    public int amount;
    public InventorySlot( int _id, ItemObject _item, int _amount)
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
