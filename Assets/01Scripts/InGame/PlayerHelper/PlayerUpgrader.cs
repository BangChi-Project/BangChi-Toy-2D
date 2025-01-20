using System;
using UnityEngine;

public class PlayerUpgrader : MonoBehaviour
{
    public int Atk { get; set; }
    public int AtkUpgradeCost { get; set; }
    
    public int Hp { get; set; }
    public int HpUpgradeCost { get; set; }

    private void Awake()
    {
        Atk = 0;
        Hp = 0;
    }

    public void AtkUpgrade()
    {
        Atk += 15;
        AtkUpgradeCost += 1;
    }

    public void HpUpgrade()
    {
        Hp += 50;
        HpUpgradeCost += 1;
    }

    public void Initialize()
    {
        Atk = 0;
        AtkUpgradeCost = 1;

        Hp = 0;
        HpUpgradeCost = 1;
    }
}
