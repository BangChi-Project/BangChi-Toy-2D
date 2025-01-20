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

    private void OnClickUpgradeButton(int id)
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
        // 로직상 Initialize가 여러번 호출 되어 AddListener 이벤트가 중복으로 추가될 수 있다.
        // 이 때 한번만 클릭해도 이벤트가 여러번 실행 될 수 있다.
        if (atkUpgradeButton != null)
            atkUpgradeButton.onClick.RemoveAllListeners(); // 모든 이벤트 제거
        if (hpUpgradeButton != null)
            hpUpgradeButton.onClick.RemoveAllListeners(); // 모든 이벤트 제거

        // 자식 오브젝트에 있는 모든 TMP를 가져옴
        var tmps = GetComponentsInChildren<TextMeshProUGUI>();
        atkUpgradeText = tmps[0];
        atkUpgradeText.SetText("ATK +0");
    
        hpUpgradeText = tmps[2]; // 버튼에도 tmp있으므로 한칸 건너뜀
        hpUpgradeText.SetText("HP +0");
    
        // 자식 오브젝트에 있는 모든 버튼을 가져옴
        var btns = GetComponentsInChildren<Button>();
        atkUpgradeButton = btns[0]; // 공격력 업그레이드 버튼
        atkUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Gold\n{InGameManager.Instance.playerUpgrader.AtkUpgradeCost.ToString()}");
        atkUpgradeButton.onClick.AddListener(() => OnClickUpgradeButton(0)); // id:0 is gold
        
        hpUpgradeButton = btns[1]; // 체력 업그레이드 버튼
        hpUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Gem\n{InGameManager.Instance.playerUpgrader.HpUpgradeCost.ToString()}");
        hpUpgradeButton.onClick.AddListener(() => OnClickUpgradeButton(1)); // id:1 is gem
    }
}
