
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUpgrader: MonoBehaviour
{
    [SerializeField] private Button hideUpgradeButton;
    [SerializeField] private TextMeshProUGUI plusAttackText;
    [SerializeField] private Button atkUpgradeButton;
    [SerializeField] private TextMeshProUGUI lobbyMoneyText;

    void OnEnable()
    {
        hideUpgradeButton.onClick.AddListener( () => OnClickHide(this.gameObject) );
        // upgrade Button
        atkUpgradeButton.onClick.AddListener(() => OnClickUpgradeButton(UpgradeType.LobbyAtk) );
        
        lobbyMoneyText.SetText(ItemCollector.Instance.GetItemAmount(ItemType.LobbyMoney).ToString("0000"));
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
        // switch
        ItemType itemType = UpgradeMatching.match[upgradeType];
        if (UpgradeExcute(upgradeType))
        {
            plusAttackText.SetText($"ATK +{PlayerUpgradeStat.Instance.LobbyAtk}");
            lobbyMoneyText.SetText(ItemCollector.Instance.GetItemAmount(ItemType.LobbyMoney).ToString("0000"));
            int cost = PlayerUpgradeStat.Instance.GetCost(upgradeType);
            atkUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"itemType:{itemType}\ncost:{cost}");
        }
        else
        {
            int cost = PlayerUpgradeStat.Instance.GetCost(upgradeType);
            Debug.Log($"Can't upgrade itemType:{itemType} cost:{cost}");
        }
    }
    
    public bool UpgradeExcute(UpgradeType id)
    {
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