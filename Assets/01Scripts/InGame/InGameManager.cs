using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [Header("SingleTon")] private static InGameManager _instance = null;
    
    [Header("Player Pos")] [Tooltip("for Enemy Chasing")] private Vector3 _playerPos {get; set;}

    // 
    public string enemyTag = "Enemy";
    public string weaponTag = "Weapon";
    
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
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [Header("Player Data")] [SerializeField]
    GameObject player;

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        _playerPos = player.transform.position;
    }
    
    [Tooltip("Getter")]
    public Vector3 GetPlayerPos()
    {
        return _playerPos;
    }
}
