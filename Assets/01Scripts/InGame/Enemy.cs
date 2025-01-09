using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract void TakeDamage(float damage);
    public abstract void Initialize();
    
    [SerializeField] protected UIHandler uiHandler;
    
    public string EnemyName {get; set;}
    public float Health {get; set;}
    public float MaxHealth {get; set;}
    public float MoveSpeed {get; set;}
    public float Atk { get; set; }

    public virtual void Death()
    {
        Destroy(gameObject);
    }
}
