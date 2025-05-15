using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager: MonoBehaviour
{
    private static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.Log("instance is null, new GameObj");
                GameObject obj = new GameObject("DataManager", typeof(DataManager));
                instance = obj.GetComponent<DataManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        Debug.Log("DataManager Awake");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        if (SkinDatas == null)
            SkinDatas = Resources.Load<SkinDatas>("SkinDic");
    }

    public SkinDatas SkinDatas = null;

    public string EquipWeaponSkin
    {
        get { return PlayerPrefs.GetString("EquipWeaponSkin", "ShortDagger"); }
        set { PlayerPrefs.SetString("EquipWeaponSkin", value); }
    }
    public string EquipArmorSkin
    {
        get
        {
            return PlayerPrefs.GetString("EquipArmorSkin", "PirateCostume");
        }
        set
        {
            PlayerPrefs.SetString("EquipArmorSkin", value);
        }
    }
}