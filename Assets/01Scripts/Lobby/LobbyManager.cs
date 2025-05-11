using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
// using SkinDic = SkinInt2String;

public class LobbyManager : MonoBehaviour
{
    // Event
    // public Action<StateEnum> OnStateChange;
    
    // Properties
    public static LobbyManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    // public StateEnum GameState { get; private set; } = StateEnum.Running;
    
    [Header("SingleTon")] private static LobbyManager instance = null;
    
    private bool isReady = false;
    [SerializeField] private LobbyView lobbyView;
    [SerializeField] private Canvas canvas;
    
    private StageDataParsing stageDataParsing;
    public StageDataList StageList { get; set; }
    public StageData CurrentStage { get; set; }
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this.gameObject); // Error when use <this>
        }
    }

    void Start()
    {
        stageDataParsing = GetComponent<StageDataParsing>();
        stageDataParsing.LoadStageData();
        
        Initialize();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Game::Scene Loaded::"+scene.name);
        if (scene.name == SceneNames.Lobby)
        {
            // canvas.gameObject.SetActive(true);
            lobbyView.gameObject.SetActive(true);
            // if (instance != null && isReady)
            //     stageContents.SetContents();
            lobbyView.SetWorldCamera();
        }
        else
        {
            lobbyView.gameObject.SetActive(false);
            // canvas.gameObject.SetActive(false);
        }
    }
    void OnDestroy()
    {
        // 이벤트 구독 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Initialize()
    {
        lobbyView.Initialize();
        
        isReady = true;
    }
}
