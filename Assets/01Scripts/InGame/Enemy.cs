using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    public abstract void TakeDamage(float damage);
    public abstract void Initialize();
    
    [FormerlySerializedAs("StateUIHandler")] [SerializeField] protected StateUIHandler stateUIHandler;
    [SerializeField] protected Item gold;
    [SerializeField] protected Item gem;
    
    public string EnemyName {get; set;}
    public float Health {get; set;}
    public float MaxHealth {get; set;}
    public float MoveSpeed {get; set;}
    public float Atk { get; set; }

    public InGameManager.StateEnum State { get; set; } = InGameManager.StateEnum.Running;

    public virtual void Death()
    {
        Destroy(gameObject);
    }

    public void OnEnable()
    {
        InGameManager.Instance.OnStateChange += OnStateChange;
    }

    public void OnDisable()
    {
        InGameManager.Instance.OnStateChange -= OnStateChange;
    }

    private void OnStateChange(InGameManager.StateEnum state)
    {
        State = state;
    }
}
