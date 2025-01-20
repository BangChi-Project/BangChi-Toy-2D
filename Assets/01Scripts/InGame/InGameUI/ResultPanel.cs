using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    private string text = "";

    void Start()
    {
        
    }
    public void ShowResultPanel()
    {
        this.gameObject.SetActive(true);
        text = "Result!\n" + InGameManager.Instance.GameTime.ToString("0.0") + " Sec";
        resultText.SetText(text);
    }
    
    public void OnClickBackToLobby()
    {
        SceneManager.LoadScene("Test_Lobby");
    }
}
