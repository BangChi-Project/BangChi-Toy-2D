using TMPro;
using UnityEngine;

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
        text = "Result!\n" + InGameManager.Instance.GameTime + " Sec";
        resultText.SetText(text);
    }
}
