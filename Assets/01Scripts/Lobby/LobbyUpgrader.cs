
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUpgrader: MonoBehaviour
{
    [SerializeField] private Button hideUpgradeButton;
    [SerializeField] private TextMeshProUGUI plusAttackText;
    [SerializeField] private Button atkUpgradeButton;

    void OnEnable()
    {
        hideUpgradeButton.onClick.AddListener( () => OnClickHide(this.gameObject) );
        // upgrade Button
        atkUpgradeButton.onClick.AddListener(() => OnClickUpgradeButton(UpgradeType.LobbyAtk) );
    }

    void OnDisable()
    {
        hideUpgradeButton.onClick.RemoveAllListeners();
        // upgrade Button
        atkUpgradeButton.onClick.RemoveAllListeners();
    }

    void OnClickHide(GameObject panel)
    {
        panel.SetActive(false);
    }
    
    public void OnClickUpgradeButton(UpgradeType upgradeType)
    {
        ItemType itemType = UpgradeMatching.match[upgradeType];
        int cost = PlayerUpgradeStat.Instance.GetCost(upgradeType);
        if (UpgradeExcute(upgradeType))
        {
            plusAttackText.SetText($"ATK +{PlayerUpgradeStat.Instance.InGameAtk}");
            
            atkUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"upgradeType:{upgradeType}\ncost:{cost}");
        }
        else
        {
            Debug.Log($"Can't upgrade upgradeType:{upgradeType} cost:{cost}");
        }
        // switch (upgradeType) // Update Text
        // {
        //     case(UpgradeType.):
                // break;
            // case((int)UsingType.LobbyHp):
            //     cost = PlayerUpgradeStat.Instance.InGameHpUpgradeCost;
            //     if (InGameManager.Instance.UpgradeExcute(id, cost))
            //     {
            //         hpUpgradeText.SetText($"HP +{PlayerUpgradeStat.Instance.InGameHp}");
            //         var hpUpgradeCost = PlayerUpgradeStat.Instance.InGameHpUpgradeCost;
            //         hpUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Gem\n{hpUpgradeCost}");
            //     }
            //     else
            //     {
            //         Debug.Log($"Can't upgrade id:{id} cost:{cost}");
            //     }
            //     break;
        //     default:
        //         break;
        // }
    }
    
    public bool UpgradeExcute(UpgradeType id)
    {
        int cost = PlayerUpgradeStat.Instance.GetCost(id);
        if (ItemCollector.Instance.UpgradeExcute(id))
        {
            switch (id)
            {
                case(UpgradeType.LobbyAtk):
                    PlayerUpgradeStat.Instance.AtkUpgrade(false);
                    return true;
                case(UpgradeType.LobbyHp):
                    PlayerUpgradeStat.Instance.HpUpgrade(false);
                    return true;
            }
        }
        return false;
    }
}