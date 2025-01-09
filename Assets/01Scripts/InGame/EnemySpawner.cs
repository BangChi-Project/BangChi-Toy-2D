using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Monster Prefabs")]
    [Tooltip("Select to use Monster Prefabs")]
    [SerializeField] GameObject monsterPrefab;
    [Tooltip("spawnDelay")]
    [SerializeField] float spawnDelay = 1f;
    float spawnTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime; // deltaTime is not be affected by frame
        if (spawnTimer >= spawnDelay)
        { 
            spawnTimer = 0f;
            Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        }
    }
}
