using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SkinDatas", menuName = "ScriptableObjects/SkinDic")]
public class SkinDatas : ScriptableObject
{
    // 인스펙터에서 편집할 카테고리별 스킨 리스트
    [SerializeField]
    private List<SkinCategoryEntry> entries = new List<SkinCategoryEntry>();

    // 런타임에 한 번 변환해 캐싱해 둘 딕셔너리
    private Dictionary<string, List<string>> _skinDictionary;
    public Dictionary<string, List<string>> SkinDictionary =>
        _skinDictionary ??= entries.ToDictionary(e => e.category, e => e.skins);
    
    public List<string> this[string category] => SkinDictionary[category];
}

[Serializable]
public class SkinCategoryEntry
{
    // 예: "Hair", "Body", "Face" 같은 스킨 카테고리
    public string category;

    // 해당 카테고리에 속하는 스킨 이름 리스트
    public List<string> skins;
}