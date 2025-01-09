using UnityEngine;

public class TreeMonster : Enemy
{
    public override void TakeDamage(float damage)
    {
        Health -= damage;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MaxHealth = 100;
        Health = MaxHealth;
        MoveSpeed = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Initialize()
    {
        MaxHealth = 100;
        Health = MaxHealth;
        MoveSpeed = 1.5f;
    }
}
