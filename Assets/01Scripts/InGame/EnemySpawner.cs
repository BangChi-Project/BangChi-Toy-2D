using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Monster Prefabs")]
    [Tooltip("Select to use Monster Prefabs")]
    private int enemyId;
    
    [Tooltip("spawnDelay")]
    [SerializeField] float spawnDelay = 1f;
    float t = 0f;
    
    bool isReady = false;

    // Update is called once per frame
    void Update()
    {
        if (isReady && InGameManager.Instance.GameState == InGameManager.StateEnum.Running)
        {
            t += Time.deltaTime; // deltaTime is not be affected by frame
            if (t >= spawnDelay)
            { 
                t = 0f;
                EnemyViewModel enemy = InGameManager.Instance.poolManager.GetEnemyInPool(enemyId);
                enemy.transform.position = transform.position;
                enemy.Initialize();
            }
        }
    }

    public void Initialize(int id, float delay)
    {
        enemyId = id;
        spawnDelay = delay;
        
        isReady = true; // start Update
    }
}
