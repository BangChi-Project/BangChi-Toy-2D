using UnityEngine;

public class EnemyViewModel:MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private ObjectView enemyView;
    
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.State == InGameManager.StateEnum.Running)
        {
            enemy.Moving();
            // _rb.AddForce(dir * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
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

    private void OnStateChange(InGameManager.StateEnum state)
    {
        enemy.HandleOnStateChange(state);
    }
    
    public void TakeDamage(float damage)
    {
        enemy.TakeDamage(damage);
    }

    public void Death() // enemy
    {
        enemy.Death();
    }
    
    private void OnTriggerStay2D (Collider2D other)
    {
        if (other.CompareTag(InGameManager.Instance.playerTag))
        {
            other.GetComponent<PlayerViewModel>().TakeDamage(enemy.Atk);
        }
    }

    public void PresentDamageText(float damage)
    {
        enemyView.PresentDamageText(damage);
    }

    public void SetHpBar(float value)
    { 
        enemyView.SetHpBar(value);
    }

    public void Initialize()
    {
        enemy.Initialize();
    }
}