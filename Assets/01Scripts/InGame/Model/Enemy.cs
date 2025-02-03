using UnityEngine;
using UnityEngine.Serialization;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyViewModel enemyViewModel;

    // [SerializeField] protected RuntimeAnimatorController[] enemiesAnimator;
    // protected Animator anim; // anim.runtimeAnimator
    
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

    protected void DropItem_A() // Gold : Gem = 1 : 1
    {
        if (Random.value < 0.5f)
        {
            Item it = InGameManager.Instance.poolManager.GetItemInPool(0);
            it.Initialize(0, "Gold",1);  // Random.Range(10, 100));
            it.transform.position = transform.position;
            
            // Instantiate(gold, transform.position, Quaternion.identity)
            //     .Initialize(0, "Gold",1);  // Random.Range(10, 100));
        }
        else
        {
            Item it = InGameManager.Instance.poolManager.GetItemInPool(1);
            it.Initialize(1, "Gem",1);  // Random.Range(10, 100));
            it.transform.position = transform.position;
        }
    }
}
