// using System;

using System.Collections.Generic;
using UnityEngine;

public class GameMaker : MonoBehaviour
{
    // Assets/Resources
    private Enemy[] enemies;
    private Item[] items;

    // initialize
    private EnemySpawner enemySpawner;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Initialize(List<SpawnerData> spawnerList)
    {
        enemies = Resources.LoadAll<Enemy>("Enemies");
        Debug.Log("Enemies Load: " + enemies.Length);

        enemySpawner = Resources.Load<EnemySpawner>("Enemies/EnemySpawner");
        
        items = Resources.LoadAll<Item>("");
        Debug.Log("Items Load: " + items.Length);
        
        foreach (var spawner in spawnerList)
        {
            Debug.Log("monId: "+spawner.monsterId + ", del: "+spawner.delay + ", pos: "+spawner.position);
            Instantiate(enemySpawner, spawner.position, Quaternion.identity).Initialize(enemies[spawner.monsterId], spawner.delay);
        }
    }
}
