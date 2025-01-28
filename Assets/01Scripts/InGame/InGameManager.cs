using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

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
            Destroy(this);
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
        UnityEngine.Debug.Log("InGame::Scene Loaded::"+scene.name);
        if (scene.name == "Test_Lobby")
        {
            GameState = StateEnum.End;
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
        
        PlayerUpgradeStat.Initialize();
        itemCollector.Initialize();
        InGuiViewModel.Instance.Initialize();

        GameState = StateEnum.Running;
    }
}
