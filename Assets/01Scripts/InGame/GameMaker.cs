// using System;

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameMaker : MonoBehaviour
{
    // initialize
    private EnemySpawner enemySpawner;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Initialize(List<SpawnerData> spawnerList)
    {
        enemySpawner = Resources.Load<EnemySpawner>("Enemies/EnemySpawner");
        foreach (var spawner in spawnerList)
        {
            Debug.Log("monId: "+spawner.monsterId + ", del: "+spawner.delay + ", pos: "+spawner.position);
            Instantiate(enemySpawner, spawner.position, Quaternion.identity).Initialize(
                spawner.monsterId, spawner.delay);
        }
    }
}
