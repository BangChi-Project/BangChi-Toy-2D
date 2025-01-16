using System.Collections.Generic;
using TMPro;
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
        bool isAdd = true;
        foreach (Item item in InVentory)
        {
            if (item.IdNumber == addItem.IdNumber)
            {
                item.AddAmount(addItem.Amount);
                isAdd = false;
            }
        }

        if (isAdd)
        {
            InVentory.Add(addItem);
            InVentory.Sort();
        }

        string itemText = "";
        foreach (Item item in InVentory)
        {
            itemText += "item name: " + item.Name + "\nitem id: " + item.IdNumber + "\nitem amount: " + item.Amount + "\n\n";
        }
        InGameUIManager.Instance.getItemsText.SetText(itemText);
    }

    private void Initialize()
    {
        InVentory = new List<Item>();
    }
}
