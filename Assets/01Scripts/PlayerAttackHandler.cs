using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    AttackRangeChecker attackRangeChecker;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackRangeChecker = GetComponentInChildren<AttackRangeChecker>();
        attackRangeChecker.OnEnemyEnter += HandleEnemyEnter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Event
    // private void OnEnable()
    private void HandleEnemyEnter(Collider2D enemy)
    {
        Debug.Log($"Entered {enemy.gameObject.tag}!");
    }
}
