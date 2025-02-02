
using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager: MonoBehaviour
{
    // Assets/Resources
    private EnemyViewModel[] enemyPrefabs;
    private Item[] itemPrefabs;
    
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

        EnemyViewModel enemyVM = Instantiate(enemyPrefabs[id], transform.position, Quaternion.identity, this.transform);
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
        Item item = Instantiate(itemPrefabs[id], transform.position, Quaternion.identity, this.transform);
        itemsPool[id].Add(item);
        return item;
    }

    public EnemyViewModel GetEnemyPrefabs(int id)
    {
        return enemyPrefabs[id];
    }

    public Item GetItemPrefabs(int id)
    {
        return itemPrefabs[id];
    }

    public void SetDisableAllObject()
    {
        Debug.Log("Set DisableAllObject");
        foreach (var enemies in enemiesPool)
        {
            foreach (var enemy in enemies)
                enemy.gameObject.SetActive(false);
        }

        foreach (var items in itemsPool)
        {
            foreach (var item in items)
                item.gameObject.SetActive(false);
        }
    }
    
    public void Initialize()
    {
        LoadEnemies();
        
        LoadItems();
    }
    
    private void LoadEnemies()
    {
        enemyPrefabs = Resources.LoadAll<EnemyViewModel>("Enemies");
        Debug.Log("Enemies Load: " + enemyPrefabs.Length);
        
        enemiesPool = new List<EnemyViewModel>[enemyPrefabs.Length];
        for (var i = 0; i < enemyPrefabs.Length; i++)
        {
            enemiesPool[i] = new List<EnemyViewModel>();
        }
    }

    private void LoadItems()
    {
        itemPrefabs = Resources.LoadAll<Item>("");
        Debug.Log("Items Load: " + itemPrefabs.Length);

        itemsPool = new List<Item>[itemPrefabs.Length];
        for (var i = 0; i < itemPrefabs.Length; i++)
        {
            itemsPool[i] = new List<Item>();
        }
    }
}