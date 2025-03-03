using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class InGameManager : MonoBehaviour
{
    public enum StateEnum
    {
        Start,
        Running,
        Pause,
        End,
    }
    
    // constant field
    public string enemyTag = "Enemy";
    public string weaponTag = "Weapon";
    public string playerTag = "Player";
    public string itemTag = "Item";

    public bool isTestMode;
    
    // GameMaker Data
    public StageData stageData;
    private GameMaker gameMaker;
    
    // Event
    public Action<StateEnum> OnStateChange;
    
    // Properties
    public static InGameManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }
    [Header("Player Pos")] [Tooltip("for Enemy Chasing")]
    public Vector3 PlayerPos
    {
        get { return playerObj.transform.position; } // Get
    }
    public StateEnum GameState { get; private set; } = StateEnum.Running;
    public float GameTime { get; private set; } = 0f;
    public PoolManager poolManager;
    public PlayerUpgradeStat PlayerUpgradeStat { get; private set; }
    
    // private field
    [Header("Player Data")]
    [SerializeField] GameObject playerPrefab;
    GameObject playerObj;
    [SerializeField] private PlayerViewModel playerViewModel;
    [SerializeField] private InGameItemCollector itemCollector;
    [Header("SingleTon")] private static InGameManager instance = null;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneLoaded;
            // Initialize();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        switch (GameState)
        {
            case(StateEnum.Start):
                break;
            case(StateEnum.Running):
                // PlayerPos = playerObj.transform.position;
                GameTime += Time.deltaTime;
                break;
            case(StateEnum.Pause):
                break;
            case(StateEnum.End):
                InGuiViewModel.Instance.ShowResultPanel();
                break;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Test_Lobby") // when go to Lobby
        {
            GameState = StateEnum.End;
            
            poolManager.DestroyAllObject();
        }
        else
        {
            if (instance != null)
                Initialize();
        }
    }
    void OnDestroy()
    {
        // 이벤트 구독 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    [Tooltip("Setter")]
    public void SetGameState(StateEnum state)
    {
        if (GameState != StateEnum.End)
        {
            GameState = state;
            OnStateChange?.Invoke(state);
        }
    }

    public void AddItem(Item item)
    {
        itemCollector.AddItem(item);
    }

    public bool UpgradeExcute(int id, int cost)
    {
        if (itemCollector.UpgradeExcute(id, cost))
        {
            switch (id)
            {
                case(0):
                    PlayerUpgradeStat.AtkUpgrade();
                    return true;
                case(1):
                    PlayerUpgradeStat.HpUpgrade();
                    playerViewModel.UpdateHpBar();
                    return true;
            }
        }
        return false;
    }

    private void Initialize()
    {
        GameTime = 0f;
        GameState = StateEnum.Start;
        
        // playerObj = Instantiate(playerObj, , Quaternion.identity);
        playerObj = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        playerViewModel = playerObj.GetComponentInChildren<PlayerViewModel>();
        PlayerUpgradeStat = GetComponent<PlayerUpgradeStat>();
        if (GameManager.Instance != null)
            playerViewModel.SetSkin("Weapon", GameManager.Instance.weaponIdx);
        
        poolManager = GetComponentInChildren<PoolManager>();
        gameMaker = GetComponentInChildren<GameMaker>();
        // SpawnerDatas = new ();
        // SpawnerDatas.Add(new SpawnerData(1, new Vector3(5, 5, 0), 2f));
        // SpawnerDatas.Add(new SpawnerData(1, new Vector3(-7, 0, 0), 3f));

        if (isTestMode) // spawner test
        {
            stageData = new StageData();
            stageData.stageName = "TestMode";
            stageData.stageId = 99;
            stageData.spawners = new List<SpawnerData>();
            
            SpawnerData spawnerData = new SpawnerData();
            spawnerData.monsterId = 1;
            spawnerData.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f);
            spawnerData.delay = 2f;
            
            stageData.spawners.Add(spawnerData);
        }
        else
        {
            stageData = GameManager.Instance.CurrentStage;
        }
        Debug.Log("spawnerCount: "+stageData.spawners.Count);
        poolManager.Initialize();
        gameMaker.Initialize(stageData.spawners);
        PlayerUpgradeStat.Initialize();
        itemCollector.Initialize();
        InGuiViewModel.Instance.Initialize();

        GameState = StateEnum.Running;
    }
}
