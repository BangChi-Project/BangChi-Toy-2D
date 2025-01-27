using UnityEngine;

public class WeaponModelView:MonoBehaviour
{
    [SerializeField] private Weapon weapon; 
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
        weapon.HandleOnStateChange(state);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Delete or any effect
        if (other.CompareTag(InGameManager.Instance.enemyTag))
        {
            other.GetComponent<Enemy>().TakeDamage(weapon.Atk);
            // Destroy(other.gameObject);
        }
    }

    public void Initialize(Collider2D target, float atk)
    {
        weapon.Initialize(target, atk);
    }
}