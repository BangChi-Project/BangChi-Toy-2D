using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGuiManager: MonoBehaviour
{
    private static InGuiManager instance;
    
    // 모델
    [SerializeField] PlayerUpgradeStat upgradeStat;
    
    // 뷰
    [SerializeField] InGuiView inGuiView;
    
    public static InGuiManager Instance
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
    
    public void UpdateItemText()
    {
        string itemText = ""; 
        List<ItemData> Inventory = ItemCollector.Instance.Inventory;
        foreach (ItemData item in Inventory)
        {
            itemText += "item name: " + item.Name + "\nitem id: " + item.IdNumber + "\nitem amount: " + item.Amount + "\n\n";
        }
        SetEarnItemText(itemText);
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (instance == null)
            return;
        
        if (scene.name == SceneNames.Lobby)
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
        
        UpdateItemText();
    }
}