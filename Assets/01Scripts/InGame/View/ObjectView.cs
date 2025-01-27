using UnityEngine;

public class ObjectView : MonoBehaviour
{
    [SerializeField] private RectTransform hpBar;
    [SerializeField] private DamageText damageText;

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
        DamageText newDamageText = Instantiate(damageText, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        newDamageText.Initialize(damage);
    }

    public void SetHpBar(float value)
    {
        hpBar.localScale = new Vector3(value, hpBar.localScale.y, hpBar.localScale.z);
    }
}
