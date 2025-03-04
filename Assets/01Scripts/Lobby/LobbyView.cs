using System;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

using SkinDic = SkinInt2String;

public class LobbyView: MonoBehaviour
{
    [SerializeField] private StageContents stageContents;
    [SerializeField] private Button showCharacterEditButton;
    [SerializeField] private Button showUpgradeButton;
    [SerializeField] private Canvas canvas;
    
    // Character Edit Panel
    [SerializeField] private GameObject characterEditPanel;
    [SerializeField] private CharacterBuilder characterBuilder;
    [SerializeField] private TMP_Dropdown weaponDropdown;
    [SerializeField] private TMP_Dropdown armorDropdown;
    [SerializeField] private Button hideCharacterEditButton;
    
    // Character Upgrade Panel
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private Button attackUpgradeButton;
    [SerializeField] private TextMeshProUGUI plusAttackText;
    [SerializeField] private Button hideUpgradeButton;

    void OnEnable()
    {
        showCharacterEditButton.onClick.AddListener(() => OnClickShow(characterEditPanel));
        hideCharacterEditButton.onClick.AddListener(() => OnClickHide(characterEditPanel));
        
        showUpgradeButton.onClick.AddListener( () => OnClickShow(upgradePanel));
        hideUpgradeButton.onClick.AddListener( () => OnClickHide(upgradePanel));
        
        weaponDropdown.onValueChanged.AddListener(value => SetWeaponIndex(value));
        armorDropdown.onValueChanged.AddListener(value => SetArmorIndex(value));
    }
    void OnDisable()
    {
        showCharacterEditButton.onClick.RemoveAllListeners();
        hideCharacterEditButton.onClick.RemoveAllListeners();
        
        showUpgradeButton.onClick.RemoveAllListeners();
        hideCharacterEditButton.onClick.RemoveAllListeners();
        
        weaponDropdown.onValueChanged.RemoveAllListeners();
        armorDropdown.onValueChanged.RemoveAllListeners();
    }

    public void OnClickShowCharacterEditPanel()
    {
        characterEditPanel.SetActive(true);
    }

    public void OnClickHideCharacterEditPanel()
    {
        characterEditPanel.SetActive(false);
    }

    public void OnClickShow(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void OnClickHide(GameObject obj)
    {
        obj.SetActive(false);
    }

    void SetWeaponIndex(int index)
    {
        GameManager.Instance.SkinString["Weapon"] = SkinDic.SkinDic["Weapon"][index];
        characterBuilder.Weapon = SkinDic.SkinDic["Weapon"][index];
        characterBuilder.Rebuild();
    }
    void SetArmorIndex(int index)
    {
        GameManager.Instance.SkinString["Armor"] = SkinDic.SkinDic["Armor"][index];
        characterBuilder.Armor = SkinDic.SkinDic["Armor"][index];
        characterBuilder.Rebuild();
    }
    
    public void Initialize()
    {
        characterEditPanel.SetActive(false);
        upgradePanel.SetActive(false);
        stageContents.SetContents();
    }

    public void SetWorldCamera()
    {
        canvas.worldCamera = Camera.main;
    }
}