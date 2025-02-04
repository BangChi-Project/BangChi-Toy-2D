using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageContent : MonoBehaviour
{
    private StageData stage;
    private Button button;
    public void Initialize(StageData stageData)
    {
        stage = stageData;
        button = GetComponent<Button>();
             
        SetButtonText();
    }
        
    public void SetButtonText()
    {
        button.onClick.AddListener(OnClickGameStart);
        TextMeshProUGUI txt = button.GetComponentInChildren<TextMeshProUGUI>();
        string text = "Stage: " + stage.stageName + "\nSpawner Count: " + stage.spawners.Count;
        txt.SetText(text);
    }

    private void OnClickGameStart()
    {
        if (InGameManager.Instance)
            InGameManager.Instance.isTestMode = false; // TestMode = false
        GameManager.Instance.CurrentStage = stage;
        SceneManager.LoadScene("InGame");
    }

    private void OnDestroy()
    {
        // if (btn != null)
        // btn.onClick.RemoveAllListeners(); // 모든 이벤트 제거
        button.onClick.RemoveAllListeners();
    }
}