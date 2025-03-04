using System;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;
using UnityEngine;

using SkinIdx = SkinInt2String;

public class PlayerViewModel: MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerAnimView animView;
    [SerializeField] private ObjectView stateView;
    [SerializeField] private CharacterBuilder builderView;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(InGameManager.Instance.itemTag)) // GetItem
        {
            var item = other.GetComponent<Item>();
            InGameManager.Instance.AddItem(item);
            item.SetDisable(); // SetActive(false)
        }
    }

    public void OnEnable()
    {
        InGameManager.Instance.OnStateChange += OnStateChange;
    }
    public void OnDisable()
    {
        if (InGameManager.Instance != null)
            InGameManager.Instance.OnStateChange -= OnStateChange;
    }

    private void OnStateChange(InGameManager.StateEnum state)
    {
        Debug.Log($"OnStateChange::playerState:{player.State}");
        player.HandleStateChange(state);
    }

    public void TakeDamage(float damage)
    {
        player.TakeDamage(damage);
    }

    public void UpdateHpBar(float value) // view
    {
        stateView.SetHpBar(value);
    }

    public void UpdateHpBar() // view
    {
        float calculHp = player.Health + InGameManager.Instance.PlayerUpgradeStat.Hp;
        float calculMaxHp = player.MaxHealth + InGameManager.Instance.PlayerUpgradeStat.Hp;
        stateView.SetHpBar(calculHp / calculMaxHp);
    }

    public void PresentDamageText(float damage)
    {
        stateView.PresentDamageText(damage);
    }

    public Vector3 GetPlayerPos()
    {
        return player.GetPlayerPos(); //////////////////////
    }

    public void SetAnimState(string state) // string, more func
    {
        animView.SetAnim(state);
    }
    public void SetAnimState(Player.StateEnum state) // Player.StateEnum
    {
        animView.SetAnim(state);
    }

    public void SetAnimTurn(float dir)
    {
        animView.SetAnimTurn(dir);
    }


    public void InGameBuildSkin()
    {
        var skinIdx = GameManager.Instance.SkinString;
        builderView.Weapon = skinIdx["Weapon"];
        builderView.Armor = skinIdx["Armor"];
        builderView.Rebuild();
    }
    // public void SetSkin(string skin, int skinIndex)
    // {
    //     builderView.Weapon = SkinIdx.WeaponNames[skin][skinIndex];
    //     builderView.Rebuild();
    // }

    // public void SetDir(string dir)
    // {
    //     animView.SetDir(dir);
    // }
}