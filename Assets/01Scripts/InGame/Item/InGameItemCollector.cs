using System.Collections.Generic;
using UnityEngine;

public class InGameItemCollector : MonoBehaviour
{
    // Properties
    public List<Item> InVentory { get; private set; }
    
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
        
        bool isAdd = true;
        foreach (Item item in InVentory)
        {
            if (item.IdNumber == addItem.IdNumber)
            {
                item.AddAmount(addItem.Amount);
                isAdd = false;
                break;
            }
        }

        if (isAdd)
        {
            InVentory.Add(addItem);
            InVentory.Sort((x, y) => x.IdNumber.CompareTo(y.IdNumber));
        }

        UpdateItemText();
    }

    public bool UpgradeExcute(int id, int cost)
    {
        if (InVentory.Count > 0)
        {
            foreach (Item item in InVentory)
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
        foreach (Item item in InVentory)
        {
            itemText += "item name: " + item.Name + "\nitem id: " + item.IdNumber + "\nitem amount: " + item.Amount + "\n\n";
        }
        InGameUIManager.Instance.getItemsText.SetText(itemText);
    }

    public void Initialize()
    {
        InVentory = new List<Item>();
        InGameUIManager.Instance.getItemsText.SetText("name + id + amount");
    }
}
