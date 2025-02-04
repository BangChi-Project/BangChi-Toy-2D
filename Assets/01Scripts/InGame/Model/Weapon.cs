using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponModelView weaponModelView;
    public InGameManager.StateEnum State { get; protected set; }
    public string WeaponName { get; protected set; }
    public float Atk { get; protected set; }
    public Vector3 TargetDir { get; protected set; }
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float deleteTime;

    public abstract void Initialize(Collider2D other, float atk); // 정의 해야함

    public virtual void Attack(Collider2D other) // override 가능
    {
        if (other.CompareTag(InGameManager.Instance.enemyTag))
        {
            other.GetComponent<Enemy>().TakeDamage(Atk);
        }
    }

    public virtual void HandleOnStateChange(InGameManager.StateEnum state)
    {
        State = state;
    }

    public virtual void Moving()
    {
        transform.position += TargetDir * (moveSpeed * Time.deltaTime);
    }
}