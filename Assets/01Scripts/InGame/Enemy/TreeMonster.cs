using UnityEngine;

public class TreeMonster : Enemy
{
    public override void TakeDamage(float damage)
    {
        ObjectView.PresentDamageText(damage);
        Health -= damage;
        if (Health <= 0)
        {
            Death();
        }
        ObjectView.SetHpBar(Health/MaxHealth);
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

    public override void Death()
    {
        if (Random.value < 0.5f)
        {
            Instantiate(gold, transform.position, Quaternion.identity)
                .Initialize(0, "Gold",1);  // Random.Range(10, 100));
        }
        else
        {
            Instantiate(gem, transform.position, Quaternion.identity)
                .Initialize(1, "Gem", 1); // Random.Range(1, 5));
        }
        Destroy(gameObject);
    }
    
    private void OnTriggerStay2D (Collider2D other)
    {
        if (other.CompareTag(InGameManager.Instance.playerTag))
        {
            other.GetComponent<PlayerViewModel>().TakeDamage(Atk);
        }
    }

    public override void Initialize()
    {
        MaxHealth = 150f;
        Health = MaxHealth;
        MoveSpeed = 1.5f;
        Atk = 2f;
    }
}
