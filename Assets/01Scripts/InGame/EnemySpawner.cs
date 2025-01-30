using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Monster Prefabs")]
    [Tooltip("Select to use Monster Prefabs")]
    private Enemy monsterPrefab;
    
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
                Instantiate(monsterPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    public void Initialize(Enemy enemy, float delay)
    {
        monsterPrefab = enemy;
        spawnDelay = delay;
        
        isReady = true; // start Update
    }
}
