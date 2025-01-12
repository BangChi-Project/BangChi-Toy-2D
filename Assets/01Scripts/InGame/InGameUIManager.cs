using Unity.VisualScripting;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    private static InGameUIManager instance;
    [SerializeField] ResultPanel resultPanel;

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
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resultPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowResultPanel()
    {
        resultPanel.ShowResultPanel();
    }
}
