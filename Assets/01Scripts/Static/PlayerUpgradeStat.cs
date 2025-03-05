using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeStat: MonoBehaviour // For InGame? or Lobby?
{
    private static PlayerUpgradeStat instance;
    public static PlayerUpgradeStat Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    [Header("InGame Stats")]
    public int InGameAtk { get; private set; }
    public int InGameAtkUpgradeCost { get; private set; } // tuple<id, cost> 
    public int InGameHp { get; private set; }
    public int InGameHpUpgradeCost { get; private set; } // tuple<id, cost>
    
    [Header("Lobby Stats")]
    public int LobbyAtk { get; set; }
    public int LobbyAtkUpgradeCost { get; private set; }
    public int LobbyHp { get; set; }
    public int LobbyHpUpgradeCost { get; private set; }

    private void Awake()
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
        
        LobbyAtk = 0;
        LobbyHp = 0;
        
        InGameAtk = 0;
        InGameHp = 0;
    }

    public void AtkUpgrade(bool isInGame)
    {
        if (isInGame)
        {
            InGameAtk += 15;
            InGameAtkUpgradeCost += 1;
        }
        else
        {
            LobbyAtk += 15;
            LobbyAtkUpgradeCost += 1;
        }
    }

    public void HpUpgrade(bool isInGame)
    {
        if (isInGame)
        {
            InGameHp += 50;
            InGameHpUpgradeCost += 1;
        }
        else
        {
            LobbyHp += 50;
            LobbyHpUpgradeCost += 1;
        }
    }

    public int CalculateInGameAtk(int atk)
    {
        return atk + LobbyAtk + InGameAtk;
    }

    public int CalculateInGameHp(int hp)
    {
        return hp + LobbyHp + InGameHp;
    }

    public int GetCost(UpgradeType idx) // Should be Match to ItemDatas::UpgradeType
    {
        switch (idx)
        {
            case UpgradeType.LobbyAtk:
                return LobbyAtkUpgradeCost;
                break;
            case UpgradeType.LobbyHp:
                return LobbyHpUpgradeCost;
                break;
            case UpgradeType.InGameAtk:
                return InGameAtkUpgradeCost;
                break;
            case UpgradeType.InGameHp:
                return InGameHpUpgradeCost;
                break;
        }

        return -1;
    }

    public void Initialize()
    {
        InGameAtk = 0;
        InGameAtkUpgradeCost = 1;

        InGameHp = 0;
        InGameHpUpgradeCost = 1;

        LobbyAtkUpgradeCost = 1;
        LobbyHpUpgradeCost = 1;
    }
}
