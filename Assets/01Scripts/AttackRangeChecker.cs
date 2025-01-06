using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class AttackRangeChecker : MonoBehaviour
{
    // this script assume that parent object have wepon?
    // AttackRange have circle collider
    private string enemyTag = "Enemy";
    
    public Action<Collider2D> OnEnemyEnter;
    // private HashSet<Collider2D> enemiesInRange = new HashSet<Collider2D>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(enemyTag))
        {
            // Debug.Log(other.gameObject.name);
            // if (enemiesInRange.Add(other))
            OnEnemyEnter?.Invoke(other);
        }
    }
}
