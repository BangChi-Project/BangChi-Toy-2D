using System;
using System.Collections.Generic;

[Serializable]
public class GetItemData
{
    public string Name;
    public int IdNumber;
    public int Amount;
    
    public void AddAmount(int amount)
    {
        Amount += amount;
    }

    public GetItemData(Item item)
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
    LobbyAtk = 100, // lobby gold
    LobbyHp = 101, // lobby gold
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