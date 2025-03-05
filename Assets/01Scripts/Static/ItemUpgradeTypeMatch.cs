using System;
using System.Collections.Generic;

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

public static class UpgradeMatching
{
    public static Dictionary<UpgradeType, ItemType> match = new ()
    {
        { UpgradeType.InGameAtk, ItemType.Gold },
        { UpgradeType.InGameHp, ItemType.Gem },
        { UpgradeType.LobbyAtk, ItemType.LobbyMoney },
        { UpgradeType.LobbyHp, ItemType.LobbyMoney },
    };
}

public enum UpgradeType
{
    InGameAtk = 0, // gold
    InGameHp = 1, // gem
    LobbyAtk = 2, // lobby gold
    LobbyHp = 3, // lobby gold
}

public enum ItemType
{
    Gold = 0,
    Gem = 1,
    LobbyMoney = 100,
}

// public enum UsingType
// {
//     ATK = 0,
//     HP = 1,
//     DEF = 2,
//     ATKSPEED = 3,
//     HPRESTORE = 4,
// }