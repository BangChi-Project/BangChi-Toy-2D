using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    private TextMeshProUGUI atkUpgradeText;
    private Button atkUpgradeButton;
    
    private TextMeshProUGUI hpUpgradeText;
    private Button hpUpgradeButton;

    private void Awake()
    {
        // Initialize();
    }

    public void OnClickUpgradeButton(int id)
    {
        int cost;
        switch (id) // Update Text
        {
            case(0):
                cost = InGameManager.Instance.playerUpgrader.AtkUpgradeCost;
                if (InGameManager.Instance.UpgradeExcute(id, cost))
                {
                    atkUpgradeText.SetText($"ATK +{InGameManager.Instance.playerUpgrader.Atk}");
                    var atkUpgradeCost = InGameManager.Instance.playerUpgrader.AtkUpgradeCost;
                    atkUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Gold\n{atkUpgradeCost}");
                }
                else
                {
                    Debug.Log($"Cant't upgrade id:{id} cost:{cost}");
                }
                break;
            case(1):
                cost = InGameManager.Instance.playerUpgrader.HpUpgradeCost;
                if (InGameManager.Instance.UpgradeExcute(id, cost))
                {
                    hpUpgradeText.SetText($"HP +{InGameManager.Instance.playerUpgrader.Hp}");
                    var hpUpgradeCost = InGameManager.Instance.playerUpgrader.HpUpgradeCost;
                    hpUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Gem\n{hpUpgradeCost}");
                }
                else
                {
                    Debug.Log($"Cant't upgrade id:{id} cost:{cost}");
                }
                break;
            default:
                break;
        }
    }

    public void Initialize()
    {
        if (atkUpgradeButton != null)
            atkUpgradeButton.onClick.RemoveAllListeners();
        if (hpUpgradeButton != null)
            hpUpgradeButton.onClick.RemoveAllListeners();
        // atkUpgradeText = GetComponentInChildren<TextMeshProUGUI>();
        // atkUpgradeButton = GetComponentInChildren<Button>();
        // atkUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText(InGameManager.Instance.playerUpgrader.AtkUpgradeCost.ToString());
        // atkUpgradeButton.onClick.AddListener(() => OnClickUpgradeButton(0)); // id:0 is gold

        var tmps = GetComponentsInChildren<TextMeshProUGUI>();
        atkUpgradeText = tmps[0];
        atkUpgradeText.SetText("ATK +0");
        
        hpUpgradeText = tmps[2];
        hpUpgradeText.SetText("HP +0");
        
        var btns = GetComponentsInChildren<Button>();
        atkUpgradeButton = btns[0];
        atkUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Gold\n{InGameManager.Instance.playerUpgrader.AtkUpgradeCost.ToString()}");
        atkUpgradeButton.onClick.AddListener(() => OnClickUpgradeButton(0)); // id:0 is gold
        
        hpUpgradeButton = btns[1];
        hpUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Gem\n{InGameManager.Instance.playerUpgrader.HpUpgradeCost.ToString()}");
        hpUpgradeButton.onClick.AddListener(() => OnClickUpgradeButton(1)); // id:1 is gem
    }
}
