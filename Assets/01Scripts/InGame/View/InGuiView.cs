using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InGuiView : MonoBehaviour
{
    [SerializeField] ResultPanel resultPanel;
    [SerializeField] UpgradePanel upgradePanel;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] public TextMeshProUGUI earnItemsText; // test
    private Button changeGameStateButton;

    // Update is called once per frame
    public void SetTimeText()
    {
        timeText.SetText(InGameManager.Instance.GameTime.ToString("0.0"));
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

    public void Initialize()
    {
        resultPanel.gameObject.SetActive(false);
        
        upgradePanel.Initialize();
    }
}
