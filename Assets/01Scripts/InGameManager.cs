using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [Header("SingleTon")] private static InGameManager _instance = null;

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
    
    [Tooltip("Getter")]
    public Vector2 GetPlayerPos() {
        return player.transform.position;
    }
}
