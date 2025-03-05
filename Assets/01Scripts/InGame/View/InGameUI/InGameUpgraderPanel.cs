using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI atkUpgradeText;
    private Button atkUpgradeButton;
    
    [SerializeField] private TextMeshProUGUI hpUpgradeText;
    private Button hpUpgradeButton;

    private void Awake()
    {
        // Initialize();
    }

    private void OnClickUpgradeButton(UpgradeType id)
    {
        int cost = PlayerUpgradeStat.Instance.GetCost(id);
        
        switch (id) // Update Text
        {
            case(UpgradeType.InGameAtk):
                if (InGameManager.Instance.UpgradeExcute(id))
                {
                    atkUpgradeText.SetText($"ATK +{PlayerUpgradeStat.Instance.InGameAtk}");
                    
                    atkUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"id:{(int)id}(Gold)\n{cost}");
                    
                    InGuiManager.Instance.UpdateItemText();
                }
                else
                {
                    Debug.Log($"Can't upgrade id:{id} cost:{cost}");
                }
                break;
            case(UpgradeType.InGameHp):
                if (InGameManager.Instance.UpgradeExcute(id))
                {
                    hpUpgradeText.SetText($"HP +{PlayerUpgradeStat.Instance.InGameHp}");
                    
                    hpUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"id:{(int)id}(Gem)\n{cost}");
                    
                    InGuiManager.Instance.UpdateItemText();
                }
                else
                {
                    Debug.Log($"Can't upgrade id:{id} cost:{cost}");
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
        
        atkUpgradeText.SetText("ATK +0");
        hpUpgradeText.SetText("HP +0");
    
        // 자식 오브젝트에 있는 모든 버튼을 가져옴
        var btns = GetComponentsInChildren<Button>();
        atkUpgradeButton = btns[0]; // 공격력 업그레이드 버튼
        atkUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Gold\n{PlayerUpgradeStat.Instance.InGameAtkUpgradeCost.ToString()}");
        atkUpgradeButton.onClick.AddListener(() => OnClickUpgradeButton(UpgradeType.InGameAtk)); // id: is gold
        
        hpUpgradeButton = btns[1]; // 체력 업그레이드 버튼
        hpUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().SetText($"Gem\n{PlayerUpgradeStat.Instance.InGameHpUpgradeCost.ToString()}");
        hpUpgradeButton.onClick.AddListener(() => OnClickUpgradeButton(UpgradeType.InGameHp)); // id: is gem
    }
}
