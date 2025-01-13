using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    AttackRangeChecker attackRangeChecker;
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
    // Event destructor
    void OnDisable()
    {
        attackRangeChecker.OnEnemyEnter -= HandleEnemyEnter; // Prevent Memory Leak
    }
    private void HandleEnemyEnter(Collider2D enemy)
    {
        if (player.State != Player.StateEnum.Attack && t > player.AttackSpeed)
        {
            Debug.Log($"Entered {enemy.name}");
            if (player.EnemyEnter(enemy))
                t = 0; // Attack Success
        }
    }
}
