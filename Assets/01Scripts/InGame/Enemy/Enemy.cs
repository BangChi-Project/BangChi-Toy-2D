using UnityEngine;
using UnityEngine.Serialization;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyViewModel enemyViewModel;
    [SerializeField] protected Item gold;
    [SerializeField] protected Item gem;
    [Header("hi")]
    [Tooltip("explain")]
    protected Rigidbody2D _rb;
    
    public string EnemyName {get; set;}
    public float Health {get; set;}
    public float MaxHealth {get; set;}
    public float MoveSpeed {get; set;}
    public float Atk { get; set; }
    
    public abstract void TakeDamage(float damage);
    public abstract void Initialize();

    public InGameManager.StateEnum State { get; set; } = InGameManager.StateEnum.Running;

    public virtual void Moving()
    {
        Vector3 playerPos = InGameManager.Instance.PlayerPos;

        Vector3 dir = (playerPos - transform.position).normalized;

        transform.position += dir * MoveSpeed * Time.deltaTime;
    }
    public virtual void Death()
    {
        Destroy(gameObject);
    }

    public void HandleOnStateChange(InGameManager.StateEnum state)
    {
        State = state;
    }
}
