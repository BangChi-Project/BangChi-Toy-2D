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
    [SerializeField] private Canvas canvas;
    
    // Character Edit Panel
    [SerializeField] private GameObject characterEditPanel;
    [SerializeField] private CharacterBuilder characterBuilder;
    [SerializeField] private TMP_Dropdown weaponDropdown;
    [SerializeField] private TMP_Dropdown armorDropdown;
    [SerializeField] private Button hideCharacterEditButton;

    void OnEnable()
    {
        weaponDropdown.onValueChanged.AddListener(value => SetWeaponIndex(value));
        armorDropdown.onValueChanged.AddListener(value => SetArmorIndex(value));
    }
    void OnDisable()
    {
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
        stageContents.SetContents();
    }

    public void SetWorldCamera()
    {
        canvas.worldCamera = Camera.main;
    }
}