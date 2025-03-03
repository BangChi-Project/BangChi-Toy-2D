using UnityEngine;
using UnityEngine.SceneManagement;

public class InGuiViewModel: MonoBehaviour
{
    private static InGuiViewModel instance;
    
    // 모델
    [SerializeField] PlayerUpgradeStat upgradeStat;
    
    // 뷰
    [SerializeField] InGuiView inGuiView;
    
    public static InGuiViewModel Instance
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

    void Start()
    {
        Initialize();
    }
    void Update()
    {
        inGuiView.SetTimeText();
    }

    public void ShowResultPanel()
    {
        inGuiView.ShowResultPanel();
    }

    public void SetEarnItemText(string text)
    {
        inGuiView.earnItemsText.SetText(text);
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (instance == null)
            return;
        
        if (scene.name == "Test_Lobby")
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            // Initialize();
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Initialize()
    {
        this.gameObject.SetActive(true);
        
        inGuiView.Initialize();
    }
}