using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 5;
    public float spawnDelay = 3f;
    public float spawnRadius = 5f; 
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnDelay && CountEnemies() < maxEnemies)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector2 randomArea = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = new Vector3(transform.position.x + randomArea.x, transform.position.y, transform.position.z + randomArea.y);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    int CountEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
}