using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    // Event
    // public Action<StateEnum> OnStateChange;
    
    // Properties
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    // public StateEnum GameState { get; private set; } = StateEnum.Running;
    
    [Header("SingleTon")] private static GameManager instance = null;
    
    public StageDataList StageList { get; set; }
    public StageData CurrentStage { get; set; }
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            // SceneManager.sceneLoaded += OnSceneLoaded;
            // Initialize();
        }
        else
        {
            Destroy(this);
        }
    }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     UnityEngine.Debug.Log("InGame::Scene Loaded::"+scene.name);
    //     if (scene.name == "Test_Lobby")
    //     {
    //         GameState = StateEnum.End;
    //     }
    //     else
    //     {
    //         if (instance != null)
    //             Initialize();
    //     }
    // }
    // void OnDestroy()
    // {
    //     // 이벤트 구독 해제
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    private void Initialize()
    {
    }
}
