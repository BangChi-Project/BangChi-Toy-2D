using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public enum StateEnum
    {
        Start,
        Playing,
        Pause,
        End,
    }
    [Header("SingleTon")] private static InGameManager _instance = null;
    
    [Header("Player Pos")] [Tooltip("for Enemy Chasing")] private Vector3 _playerPos {get; set;}

    [Header("Player Data")]
    [SerializeField] GameObject player;
    
    // 
    public string enemyTag = "Enemy";
    public string weaponTag = "Weapon";
    public string playerTag = "Player";
    public StateEnum GameState { get; private set; } = StateEnum.Playing;
    
    public float GameTime { get; private set; } = 0f;
    
    public static InGameManager Instance
    {
        get
        {
            if (_instance == null)
                return null;
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
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
            case(StateEnum.Playing):
                _playerPos = player.transform.position;
                GameTime += Time.deltaTime;
                break;
            case(StateEnum.Pause):
                break;
            case(StateEnum.End):
                InGameUIManager.Instance.ShowResultPanel();
                break;
        }
    }

    public void SetGameEnd()
    {
        GameState = StateEnum.End;
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
    
    [Tooltip("Getter")]
    public Vector3 GetPlayerPos()
    {
        return _playerPos;
    }

    void Initialize()
    {
        GameTime = 0f;
        GameState = StateEnum.Start;
        player = GameObject.FindGameObjectWithTag("Player");
        
        GameState = StateEnum.Playing;
    }
}
