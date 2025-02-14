using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;
using UnityEngine;

public class PlayerAnimView: MonoBehaviour
{
    [SerializeField] private CharacterControls characterControls;
    public void SetAnim(string state)
    {
        characterControls.SetAnim(state);
    }

    public void AnimTurn(float dir)
    {
        characterControls.Turn(dir);
    }

    public void SetDir(string dir)
    {
        characterControls.SetDir(dir);
    }
}