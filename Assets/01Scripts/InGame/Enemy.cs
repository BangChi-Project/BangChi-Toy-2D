using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public string EnemyName {get; set;}
    public float Health {get; set;}
    public float MaxHealth {get; set;}
    public float MoveSpeed {get; set;}
    public abstract void TakeDamage(float damage);
    public abstract void Initialize();
}
