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
    [SerializeField] private Canvas canvas;
    
    // Character Edit Panel
    [SerializeField] private GameObject characterEditPanel;
    [SerializeField] private CharacterBuilder characterBuilder;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Button hideCharacterEditButton;

    void OnEnable()
    {
        dropdown.onValueChanged.AddListener(value => SetIndex(value));
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
        GameManager.Instance.weaponIdx = index;
        characterBuilder.Weapon = SkinIdx.WeaponNames[index];
        characterBuilder.Rebuild();
    }
    
    public void Initialize()
    {
        characterEditPanel.SetActive(false);
        stageContents.SetContents();
    }
}