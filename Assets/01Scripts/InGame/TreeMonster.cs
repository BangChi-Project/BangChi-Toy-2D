using UnityEngine;

public class TreeMonster : Enemy
{
    public override void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Death();
        }
        uiHandler.SetHpBar(Health/MaxHealth);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    public override void Initialize()
    {
        MaxHealth = 150f;
        Health = MaxHealth;
        MoveSpeed = 1.5f;
        Atk = 0.5f;
    }
}
