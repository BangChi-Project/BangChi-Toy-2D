using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ItemData
{
    public string Name { get; set; }
    public int IdNumber { get; set; }
    public int Amount { get; set; }
    
    public void AddAmount(int amount)
    {
        Amount += amount;
    }

    public ItemData(Item item)
    {
        Name = item.Name;
        IdNumber = item.IdNumber;
        Amount = item.Amount;
    }
}

public class InGameItemCollector : MonoBehaviour
{
    // Properties
    public List<ItemData> Inventory { get; private set; }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }

    public void AddItem(Item addItem)
    {
        // Item findItem = InVentory.Find(item =>
        // {
        //     Debug.Log($"Finding item Id: {item.IdNumber} isTrue? {item.IdNumber == addItem.IdNumber}");
        //     return item.IdNumber == addItem.IdNumber;
        // });
        // if (findItem == null) // always return null,, Why???
        // {
        //     Debug.Log($"Cant find same item Id: {addItem.IdNumber} isFalse");
        // }
        
        bool isFind = true;
        foreach (ItemData item in Inventory)
        {
            if (item.IdNumber == addItem.IdNumber)
            {
                item.AddAmount(addItem.Amount);
                isFind = false;
                break;
            }
        }

        if (isFind)
        {
            Inventory.Add(new ItemData(addItem));
            Inventory.Sort((x, y) => x.IdNumber.CompareTo(y.IdNumber));
        }

        UpdateItemText();
    }

    public bool UpgradeExcute(int id, int cost)
    {
        if (Inventory.Count > 0)
        {
            foreach (ItemData item in Inventory)
            {
                if (item.IdNumber == id && item.Amount >= cost)
                {
                    item.AddAmount(-cost);
                    UpdateItemText();
                    return true;
                }
            }
        }

        return false;
    }

    public void UpdateItemText()
    {
        string itemText = "";
        foreach (ItemData item in Inventory)
        {
            itemText += "item name: " + item.Name + "\nitem id: " + item.IdNumber + "\nitem amount: " + item.Amount + "\n\n";
        }
        InGuiViewModel.Instance.SetEarnItemText(itemText);
    }

    public void Initialize()
    {
        Inventory = new List<ItemData>();
        InGuiViewModel.Instance.SetEarnItemText("name + id + amount");
    }
}
