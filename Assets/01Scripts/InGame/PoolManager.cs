
using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager: MonoBehaviour
{
    [Header("Assets/Resources")]
    [SerializeField] private EnemyViewModel[] enemyPrefabs;
    [SerializeField] private Item[] itemPrefabs;
    [SerializeField] private DamageText damageTextPrefab;
    
    [Header("Pools")]
    private List<EnemyViewModel>[] enemiesPool;
    private List<Item>[] itemsPool;
    private List<DamageText> damageTextPool;

    // Object Getter
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

    public DamageText GetDamageTextInPool()
    {
        foreach (DamageText dt in damageTextPool)
        {
            if (dt.gameObject.activeSelf == false)
            {
                dt.gameObject.SetActive(true);
                return dt;
            }
        }
        DamageText damageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity, this.transform);
        damageTextPool.Add(damageText);
        return damageText;
    }

    public EnemyViewModel GetEnemyPrefabs(int id)
    {
        return enemyPrefabs[id];
    }

    public Item GetItemPrefabs(int id)
    {
        return itemPrefabs[id];
    }

    public void DestroyAllObject()
    {
        Debug.Log("Destroy All Object");
        foreach (var enemies in enemiesPool)
        {
            foreach (var enemy in enemies)
                Destroy(enemy.gameObject);
        }

        foreach (var items in itemsPool)
        {
            foreach (var item in items)
                Destroy(item.gameObject);
        }

        foreach (var dt in damageTextPool)
        {
            Destroy(dt.gameObject);
        }
    }
    
    public void Initialize()
    {
        // Load and Set new List
        LoadEnemies();
        
        LoadItems();

        LoadDamageText();
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
        itemPrefabs = Resources.LoadAll<Item>("Items");
        Debug.Log("Items Load: " + itemPrefabs.Length);

        itemsPool = new List<Item>[itemPrefabs.Length];
        for (var i = 0; i < itemPrefabs.Length; i++)
        {
            itemsPool[i] = new List<Item>();
        }
    }

    private void LoadDamageText()
    {
        damageTextPrefab = Resources.Load<DamageText>("DamageText");
        
        damageTextPool = new List<DamageText>();
    }
}