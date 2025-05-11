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
                GameObject obj = new GameObject("DataManager", typeof(DataManager));
                instance = obj.GetComponent<DataManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            
            if (SkinDatas == null)
                SkinDatas = Resources.Load<SkinDatas>("SkinDatas");
        }
        else
        {
            Destroy(this.gameObject); // Error when use <this>
        }
    }

    public SkinDatas SkinDatas;

    public string EquipWeaponSkin
    {
        get
        {
            return PlayerPrefs.GetString("EquipWeaponSkin", "ShortDagger");
        }
        set
        {
            PlayerPrefs.GetString("EquipWeaponSkin", value);
        }
    }
    public string EquipArmorSkin
    {
        get
        {
            return PlayerPrefs.GetString("EquipArmorSkin", "PirateCostume");
        }
        set
        {
            PlayerPrefs.GetString("EquipArmorSkin", value);
        }
    }
}