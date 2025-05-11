using System;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LobbyView: MonoBehaviour
{
    [SerializeField] private StageContents stageContents;
    [SerializeField] private Button showCharacterEditButton;
    [SerializeField] private Button showUpgradeButton;
    [SerializeField] private Canvas canvas;
    
    // Character Edit Panel
    [Header("Character Edit")]
    [SerializeField] private GameObject characterEditPanel;
    [SerializeField] private CharacterBuilder characterBuilder;
    [SerializeField] private TMP_Dropdown weaponDropdown;
    [SerializeField] private TMP_Dropdown armorDropdown;
    [SerializeField] private Button hideCharacterEditButton;
    
    // Character Upgrade Panel
    [Header("Character Upgrade")]
    [SerializeField] private GameObject upgraderPanel;
    
    void OnEnable()
    {
        // Rebuild Character
        characterBuilder.Weapon = DataManager.Instance.EquipWeaponSkin;
        characterBuilder.Armor = DataManager.Instance.EquipArmorSkin;
        characterBuilder.Rebuild();
        Debug.Log($"Rebuild W:{DataManager.Instance.EquipWeaponSkin}");

        // make dropDown Button
        weaponDropdown.options.Clear();
        var a = DataManager.Instance.SkinDatas.ToString();
        foreach (var name in DataManager.Instance.SkinDatas["Weapon"])
        {
            weaponDropdown.options.Add(new TMP_Dropdown.OptionData(name));
        }
        armorDropdown.options.Clear();
        foreach (var name in DataManager.Instance.SkinDatas["Armor"])
        {
            armorDropdown.options.Add(new TMP_Dropdown.OptionData(name));
        }
        
        // show&hide Button
        showCharacterEditButton.onClick.AddListener(() => OnClickShow(characterEditPanel));
        hideCharacterEditButton.onClick.AddListener(() => OnClickHide(characterEditPanel));
        
        showUpgradeButton.onClick.AddListener( () => OnClickShow(upgraderPanel));
        
        // edit Dropdown
        weaponDropdown.onValueChanged.AddListener(value => SetWeaponIndex(value));
        armorDropdown.onValueChanged.AddListener(value => SetArmorIndex(value));
        
    }
    void OnDisable()
    {
        // show&hide Button
        showCharacterEditButton.onClick.RemoveAllListeners();
        hideCharacterEditButton.onClick.RemoveAllListeners();
        
        showUpgradeButton.onClick.RemoveAllListeners();
        hideCharacterEditButton.onClick.RemoveAllListeners();
        
        // edit Dropdown
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
        DataManager.Instance.EquipWeaponSkin = DataManager.Instance.SkinDatas["Weapon"][index];
        characterBuilder.Weapon = DataManager.Instance.SkinDatas["Weapon"][index];
        characterBuilder.Rebuild();
    }
    void SetArmorIndex(int index)
    {
        DataManager.Instance.EquipArmorSkin = DataManager.Instance.SkinDatas["Armor"][index];
        characterBuilder.Armor = DataManager.Instance.SkinDatas["Armor"][index];
        characterBuilder.Rebuild();
    }
    
    public void Initialize()
    {
        characterEditPanel.SetActive(false);
        upgraderPanel.SetActive(false);
        stageContents.SetContents();
    }

    public void SetWorldCamera()
    {
        canvas.worldCamera = Camera.main;
    }
}