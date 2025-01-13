using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public string WeaponName { get; protected set; }
    public float Atk { get; protected set; }
    public InGameManager.StateEnum State { get; protected set; } = InGameManager.StateEnum.Running;

    public abstract void Initialize(Collider2D other, float atk); // 정의 해야함

    public virtual void Attack(Collider2D other) // override 가능
    {
        if (other.CompareTag(InGameManager.Instance.enemyTag))
        {
            other.GetComponent<Enemy>().TakeDamage(Atk);
        }
    }
    
    public void OnEnable()
    {
        InGameManager.Instance.OnStateChange += OnStateChange;
    }

    public void OnDisable()
    {
        InGameManager.Instance.OnStateChange -= OnStateChange;
    }

    protected virtual void OnStateChange(InGameManager.StateEnum state)
    {
        State = state;
    }
}