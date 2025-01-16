using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public enum StateEnum
    {
        Start,
        Running,
        Pause,
        End,
    }
    
    // Instance
    [Header("SingleTon")] private static InGameManager instance = null;

    
    // constant field
    public string enemyTag = "Enemy";
    public string weaponTag = "Weapon";
    public string playerTag = "Player";
    public string itemTag = "Item";
    
    // Event
    public Action<StateEnum> OnStateChange;
    
    // Properties
    [Header("Player Pos")] [Tooltip("for Enemy Chasing")] public Vector3 PlayerPos { get; set; }
    public StateEnum GameState { get; private set; } = StateEnum.Running;
    public float GameTime { get; private set; } = 0f;
    
    // private field
    [Header("Player Data")]
    [SerializeField] GameObject player;
    [SerializeField] private InGameItemCollector itemCollector;
    
    public static InGameManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

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
            Destroy(this.gameObject);
        }
    }


// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        switch (GameState)
        {
            case(StateEnum.Start):
                break;
            case(StateEnum.Running):
                PlayerPos = player.transform.position;
                GameTime += Time.deltaTime;
                break;
            case(StateEnum.Pause):
                break;
            case(StateEnum.End):
                InGameUIManager.Instance.ShowResultPanel();
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
            Initialize();
    }
    private void OnDestroy()
    {
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

    void Initialize()
    {
        GameTime = 0f;
        GameState = StateEnum.Start;
        player = GameObject.FindGameObjectWithTag("Player");
        
        GameState = StateEnum.Running;
    }
}
