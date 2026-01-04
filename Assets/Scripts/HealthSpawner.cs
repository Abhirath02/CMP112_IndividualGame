using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    public GameObject healthPrefab;
    public int maxHealth = 5;
    public float spawnDelay = 3f;
    public float spawnRadius = 5f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnDelay && CountHealth() < maxHealth)
        {
            SpawnHealth();
            timer = 0f;
        }
    }

    void SpawnHealth() //spawn health randomly
    {
        Vector2 randomArea = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = new Vector3(transform.position.x + randomArea.x, -3f, transform.position.z + randomArea.y);

        Instantiate(healthPrefab, spawnPos, Quaternion.identity);
    }

    int CountHealth() // count the number of health prefabs
    {
        return GameObject.FindGameObjectsWithTag("Health").Length;
    }
}