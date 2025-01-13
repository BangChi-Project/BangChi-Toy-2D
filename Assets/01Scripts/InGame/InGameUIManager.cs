using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    private static InGameUIManager instance;
    [SerializeField] ResultPanel resultPanel;
    [SerializeField] TextMeshProUGUI timeText;

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
        if (scene.name == "Test_Lobby")
        {
            this.gameObject.SetActive(false);
        }
        else
            Initialize();
    }

    public void ShowResultPanel()
    {
        resultPanel.ShowResultPanel();
    }

    public void OnClickBackToLobby()
    {
        SceneManager.LoadScene("Test_Lobby");
    }

    private void Initialize()
    {
        this.gameObject.SetActive(true);
        resultPanel.gameObject.SetActive(false);
    }
}
