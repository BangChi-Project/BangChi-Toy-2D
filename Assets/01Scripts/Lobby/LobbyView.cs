using System;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

using SkinIdx = SkinInt2String;

public class LobbyView: MonoBehaviour
{
    [SerializeField] private StageContents stageContents;
    [SerializeField] private Button showCharacterEditButton;
    
    // Character Edit Panel
    [SerializeField] private GameObject characterEditPanel;
    [SerializeField] private CharacterBuilder characterBuilder;
    [SerializeField] private TMP_Dropdown dropdown;
    public int weaponIdx = 0;
    [SerializeField] private Button hideCharacterEditButton;

    void OnEnable()
    {
        dropdown.onValueChanged.AddListener(value => SetIndex(value));
    }
    
    public void Initialize()
    {
        characterEditPanel.SetActive(false);
        stageContents.SetContents();
    }

    public void OnClickShowCharacterEditPanel()
    {
        characterEditPanel.SetActive(true);
    }

    public void OnClickHideCharacterEditPanel()
    {
        characterEditPanel.SetActive(false);
    }

    void SetIndex(int index)
    {
        weaponIdx = index;
        characterBuilder.Weapon = SkinIdx.WeaponNames[weaponIdx];
        characterBuilder.Rebuild();
    }
}