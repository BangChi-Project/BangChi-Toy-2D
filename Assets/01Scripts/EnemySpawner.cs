using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Monster Prefabs")]
    [Tooltip("Select to use Monster Prefabs")]
    GameObject _monsterPrefab;
    [Tooltip("spawnDelay")]
    float _spawnDelay;
    float _spawnTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnDelay = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer += Time.deltaTime; // deltaTime is not be affected by frame
        if (_spawnTimer >= _spawnDelay)
        { 
            _spawnTimer = 0f;
            Instantiate(_monsterPrefab, transform.position, Quaternion.identity);
        }
    }
}
