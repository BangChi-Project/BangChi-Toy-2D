using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private static ItemCollector instance;
    
    // Properties
    public static ItemCollector Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    } 
    public List<ItemData> Inventory { get; private set; } = new List<ItemData>(); // 초기화하면 안되는 중요한 변수

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        Initialize();
    }

    public void AddItem(Item addItem)
    {
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
    }

    public bool UpgradeExcute(UpgradeType upgradeType)
    {
        int itemId = (int)UpgradeMatching.match[upgradeType];
        int cost = PlayerUpgradeStat.Instance.GetCost(upgradeType);
        if (Inventory.Count > 0)
        {
            foreach (ItemData item in Inventory)
            {
                if (item.IdNumber == itemId && item.Amount >= cost)
                {
                    item.AddAmount(-cost);
                    return true;
                }
            }
        }

        return false;
    }

    public int GetItemAmount(ItemType itemType)
    {
        ItemData it = Inventory.Find(x => x.IdNumber == (int)itemType);
        if (it == null)
            return 0;
        else
        {
            return it.Amount;
        }
    }

    public void Initialize()
    {
        if (Inventory.Count > 0)
            Inventory.RemoveAll(item => item.IdNumber == (int)ItemType.Gem || item.IdNumber == (int)ItemType.Gold);
    }
}
