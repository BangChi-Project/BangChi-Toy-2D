using System;
using UnityEngine;

public class PlayerUpgrader : MonoBehaviour
{
    public int Atk { get; set; }
    public int AtkUpgradeCost { get; set; }

    private void Awake()
    {
        Atk = 0;
    }

    public void UpgradeAtk()
    {
        Atk += 10;
        AtkUpgradeCost += 1;
    }

    public void Initialize()
    {
        Atk = 0;
        AtkUpgradeCost = 1;
    }
}
