using System.Collections;
using System.Net.Mime;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageTMP;

    private InGameManager.StateEnum gameState;
    private float deleteTime;
    private float moveSpeed;
    private Color alpha;
    private float disapearAlpha;

    private float t;
    
    void Awake()
    {
        t = 0;
        
        gameState = InGameManager.StateEnum.Running;
        deleteTime = 1f;
        moveSpeed = 0.8f;
        alpha = Color.red;
        disapearAlpha = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == InGameManager.StateEnum.Running)
        {
            t += Time.deltaTime;
            
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * disapearAlpha); // 서서히 1 ~> 0.3
            damageTMP.color = alpha;
        }
    }

    public void OnEnable()
    {
        InGameManager.Instance.OnStateChange += OnStateChange;
    }

    public void OnDisable()
    {
        if (InGameManager.Instance != null)
            InGameManager.Instance.OnStateChange -= OnStateChange;
    }
    
    void OnStateChange(InGameManager.StateEnum state)
    {
        // Debug.Log("DamageText::OnStateChange");
        gameState = state;
        switch (state)
        {
            case(InGameManager.StateEnum.Running):
                StartCoroutine(nameof(CoDeleteDamageText), deleteTime-t);
                break;
            case(InGameManager.StateEnum.Pause):
                StopCoroutine(nameof(CoDeleteDamageText));
                break;
            case(InGameManager.StateEnum.End):
                StopCoroutine(nameof(CoDeleteDamageText));
                break;
        }
    }

    public void SetDisable()
    {
        StopCoroutine(nameof(CoDeleteDamageText));
        
        this.gameObject.SetActive(false);
    }

    IEnumerator CoDeleteDamageText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SetDisable();
    }

    public void Initialize(float damage)
    {
        alpha.a = 1f;
        // Debug.Log($"DamageText::DeleteTime: {deleteTime}");
        damageTMP.SetText(damage.ToString("N0")); // Integer
        StartCoroutine(nameof(CoDeleteDamageText), deleteTime);
    }
}
