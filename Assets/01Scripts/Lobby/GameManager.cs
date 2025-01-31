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
    
    private StageDataParsing stageDataParsing;
    public StageDataList StageList { get; set; }
    public StageData CurrentStage { get; set; }
    [SerializeField] private StageContents stageContents;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            // SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        Initialize();
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
        stageDataParsing = GetComponent<StageDataParsing>();

        stageDataParsing.LoadStageData();
        stageContents.SetContents();
    }
}
