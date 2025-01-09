using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private RectTransform hpBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpBar.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHpBar(float value)
    {
        hpBar.localScale = new Vector3(value, hpBar.localScale.y, hpBar.localScale.z);
    }
}
