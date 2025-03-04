using UnityEngine;

public class EnemyDontMovingModel : Enemy
{
    public override void Moving()
    {
        return ;
    }
    public override void TakeDamage(float damage)
    {
        enemyViewModel.PresentDamageText(damage);
        Health -= damage;
        if (Health <= 0)
        {
            Death();
        }
        enemyViewModel.SetHpBar(Health/MaxHealth);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }

    public override void Death()
    {
        DropItem_A();
        
        // Destroy(gameObject);
        this.gameObject.SetActive(false); // for Pool
    }

    public override void Initialize()
    {
        MaxHealth = 150f;
        Health = MaxHealth;
        MoveSpeed = 1.5f;
        Atk = 2f;
    }
}
