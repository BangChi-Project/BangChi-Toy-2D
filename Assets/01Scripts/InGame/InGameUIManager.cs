using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    private static InGameUIManager instance;
    [SerializeField] ResultPanel resultPanel;
    [SerializeField] UpgradePanel upgradePanel;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] public TextMeshProUGUI getItemsText; // test

    public static InGameUIManager Instance
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

    // Update is called once per frame
    void Update()
    {
        timeText.SetText(InGameManager.Instance.GameTime.ToString("0.0"));
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
            Initialize();
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void ShowResultPanel()
    {
        resultPanel.ShowResultPanel();
    }

    public void OnClickChangeGameState(int i) // InGameManager.StateEnum state)
    {
        switch (i)
        {
            case 0: // start
                InGameManager.Instance.SetGameState(InGameManager.StateEnum.Start);
                break;
            case 1: // running
                InGameManager.Instance.SetGameState(InGameManager.StateEnum.Running);
                break;
            case 2: // pause
                InGameManager.Instance.SetGameState(InGameManager.StateEnum.Pause);
                break;
            case 3: // End
                InGameManager.Instance.SetGameState(InGameManager.StateEnum.End);
                break;
        }
    }

    private void Initialize()
    {
        this.gameObject.SetActive(true);
        resultPanel.gameObject.SetActive(false);
        
        upgradePanel.Initialize();
    }
}
