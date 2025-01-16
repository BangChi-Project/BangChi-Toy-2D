using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRangeHandler : MonoBehaviour
{
    RangeChecker rangeChecker;
    private float t=0;
    [SerializeField] Player player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rangeChecker = transform.parent.GetComponentInChildren<RangeChecker>();
        rangeChecker.OnEnemyEnter += HandleEnemyEnter;
        rangeChecker.OnItemEnter += HandleItemEnter;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GameState == InGameManager.StateEnum.Running)
            t += Time.deltaTime;
    }
    
    // Event
    // Event destructor
    void OnDisable()
    {
        rangeChecker.OnEnemyEnter -= HandleEnemyEnter; // Prevent Memory Leak
        rangeChecker.OnItemEnter -= HandleItemEnter;
    }
    private void HandleEnemyEnter(Collider2D enemy)
    {
        Debug.Log($"Handle {enemy.name}");
        if (player.GameState == InGameManager.StateEnum.Running)
        {
            if (player.State != Player.StateEnum.Attack && t > player.AttackSpeed)
            {
                Debug.Log($"Entered {enemy.name}");
                if (player.EnemyEnter(enemy))
                    t = 0; // Attack Success
            }
        }
    }

    private void HandleItemEnter(Collider2D item) // touch or Player chase Item
    {
        if (player.GameState == InGameManager.StateEnum.Running)
        {
            var it = item.GetComponent<Item>();
            InGameManager.Instance.AddItem(it);
            Destroy(item.gameObject);
        }
    }
}
