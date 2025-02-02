
using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager: MonoBehaviour
{
    // Assets/Resources
    private EnemyViewModel[] enemies;
    private Item[] items;
    
    // Poll
    private List<EnemyViewModel>[] enemiesPool;
    private List<Item>[] itemsPool;

    public EnemyViewModel GetEnemyInPool(int id)
    {
        foreach (EnemyViewModel e in enemiesPool[id])
        {
            if (e.gameObject.activeSelf == false)
            {
                e.gameObject.SetActive(true);
                return e;
            }
        }

        EnemyViewModel enemyVM = Instantiate(enemies[id], transform.position, Quaternion.identity, this.transform);
        enemiesPool[id].Add(enemyVM);
        return enemyVM;
    }
    
    public Item GetItemInPool(int id)
    {
        foreach (Item it in itemsPool[id])
        {
            if (it.gameObject.activeSelf == false)
            {
                it.gameObject.SetActive(true);
                return it;
            }
        }
        Item item = Instantiate(items[id], transform.position, Quaternion.identity, this.transform);
        itemsPool[id].Add(item);
        return item;
    }

    public void Initialize()
    {
        LoadEnemies();
        
        LoadItems();
    }
    
    private void LoadEnemies()
    {
        enemies = Resources.LoadAll<EnemyViewModel>("Enemies");
        Debug.Log("Enemies Load: " + enemies.Length);
        
        enemiesPool = new List<EnemyViewModel>[enemies.Length];
        for (var i = 0; i < enemies.Length; i++)
        {
            enemiesPool[i] = new List<EnemyViewModel>();
        }
    }

    private void LoadItems()
    {
        items = Resources.LoadAll<Item>("");
        Debug.Log("Items Load: " + items.Length);

        itemsPool = new List<Item>[items.Length];
        for (var i = 0; i < items.Length; i++)
        {
            itemsPool[i] = new List<Item>();
        }
    }

    public EnemyViewModel GetEnemyPrefabs(int id)
    {
        return enemies[id];
    }

    public Item GetItemPrefabs(int id)
    {
        return items[id];
    }
}