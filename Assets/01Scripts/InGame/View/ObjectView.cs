using UnityEngine;

public class ObjectView : MonoBehaviour
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

    public void PresentDamageText(float damage)
    {
        DamageText damageText = InGameManager.Instance.poolManager.GetDamageTextInPool(); 
        damageText.Initialize(damage);
        damageText.transform.position = transform.position + Vector3.up * 0.5f;
    }

    public void SetHpBar(float value) // 0~1f
    {
        hpBar.localScale = new Vector3(value, hpBar.localScale.y, hpBar.localScale.z);
    }

    public void Initialize()
    {
        SetHpBar(1f);
    }
}
