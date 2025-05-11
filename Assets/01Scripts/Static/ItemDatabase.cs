using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "ItemDatabase", menuName = "ScriptableObjects/ItemDatabase")]
public class ItemDatabase: ScriptableObject
{
    public ItemDTO[] itemDTOs;
}

[Serializable]
public class ItemDTO
{
    public int id;
    public string name;
    public string description;
    public Sprite icon;
    public AnimationClip animationClip;
}