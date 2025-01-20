using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    private TextMeshProUGUI atkUpgradeText;
    private Button atkUpgradeButton;

    private void Awake()
    {
        // Initialize();
    }

    public void OnClickUpgradeButton(int id)
    {
        int cost = InGameManager.Instance.playerUpgrader.AtkUpgradeCost;
        if (InGameManager.Instance.UpgradeExcute(id, cost)) // Pay Cost
        {
            switch (id)
            {
                case(0):
                    atkUpgradeText.SetText($"ATK +{InGameManager.Instance.playerUpgrader.Atk}");
                    var atkUpgradeCost = InGameManager.Instance.playerUpgrader.AtkUpgradeCost;
                    atkUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText(atkUpgradeCost.ToString());
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.Log($"Cant't upgrade id:{id} cost:{cost}");
        }
    }

    public void Initialize()
    {
        atkUpgradeText = GetComponentInChildren<TextMeshProUGUI>();
        atkUpgradeButton = GetComponentInChildren<Button>();
        atkUpgradeText.SetText("ATK +0");
        atkUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText(InGameManager.Instance.playerUpgrader.AtkUpgradeCost.ToString());
        atkUpgradeButton.onClick.AddListener(() => OnClickUpgradeButton(0)); // id:0 is gold
    }
}
