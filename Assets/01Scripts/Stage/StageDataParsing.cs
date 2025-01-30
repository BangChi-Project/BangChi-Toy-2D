using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StageDataParsing: MonoBehaviour
{
    public const string FILE_NAME = "StageDatas.json";

    bool isStageDataLoaded = false;

    private void Awake() // load Json
    {
        if (isStageDataLoaded == false)
            LoadStageData();
    }

    private void LoadStageData()
    {
        // string filePath = Application.persistentDataPath + "/" + FILE_NAME; // Library/Application Support
        string filePath = Path.Combine(Application.dataPath, FILE_NAME);
        
        if (File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath); 
            GameManager.Instance.StageList = JsonUtility.FromJson<StageDataList>(fromJsonData);
            Debug.Log("Stage count: "+GameManager.Instance.StageList.stages.Count);
        }
        else
        {
            Debug.LogError("JSON 파일이 존재하지 않습니다: " + filePath);
        }
    }
}