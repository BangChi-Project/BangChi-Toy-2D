using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    AttackRangeChecker attackRangeChecker;
    [SerializeField] private float attackCoolTime;
    private float t=0;
    [SerializeField] Player player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackRangeChecker = GetComponentInChildren<AttackRangeChecker>();
        attackRangeChecker.OnEnemyEnter += HandleEnemyEnter;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
    }
    
    // Event
    // private void OnEnable()
    private void HandleEnemyEnter(Collider2D enemy)
    {
        if (t > attackCoolTime)
        {
            t = 0;
            Debug.Log($"Entered {enemy.gameObject.tag}, Attack!");
            player.Attack(enemy);
        }
    }
}
