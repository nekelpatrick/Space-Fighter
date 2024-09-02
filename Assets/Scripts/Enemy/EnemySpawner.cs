using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the spawning of enemies at random positions relative to the player,
/// with dynamic difficulty adjustments.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float initialSpawnRate = 2.0f;
    public float spawnRateDecrease = 0.07f;
    public float minSpawnRate = 1.0f;
    public float spawnDistance = 250.0f;
    public Transform playerTransform;

    private float nextSpawn = 0.0f;
    private float currentSpawnRate;
    private HashSet<Transform> activeEnemies = new HashSet<Transform>();

    private AutoShoot autoShoot;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;

        // Find the AutoShoot component on the player
        if (playerTransform != null)
        {
            autoShoot = playerTransform.GetComponent<AutoShoot>();
            if (autoShoot == null)
            {
                Debug.LogError("AutoShoot script not found on the player!");
            }
        }
        else
        {
            Debug.LogError("Player Transform not assigned to EnemySpawner!");
        }
    }

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + currentSpawnRate;
            SpawnEnemy();

            // Gradually decrease the spawn rate to increase difficulty
            currentSpawnRate = Mathf.Max(minSpawnRate, currentSpawnRate - spawnRateDecrease * Time.deltaTime);
        }
    }

    void SpawnEnemy()
    {
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        float randomDistance = Random.Range(spawnDistance, spawnDistance + 10.0f);
        float randomX = playerTransform.position.x + Mathf.Cos(randomAngle) * randomDistance;
        float randomZ = playerTransform.position.z + Mathf.Sin(randomAngle) * randomDistance;

        Vector3 spawnPosition = new Vector3(randomX, playerTransform.position.y, randomZ);

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(enemy.transform);

        if (autoShoot != null)
        {
            autoShoot.AddEnemy(enemy.transform);
            Debug.Log($"Enemy spawned at position: {spawnPosition}. Added to AutoShoot.");
        }
    }

    /// <summary>
    /// Removes an enemy from the tracking set and the active enemies set.
    /// </summary>
    /// <param name="enemy">The enemy to remove.</param>
    public void RemoveEnemy(Transform enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            Debug.Log($"Enemy removed from active enemies: {enemy.name}");
        }

        if (autoShoot != null)
        {
            autoShoot.RemoveEnemy(enemy);
        }
    }
}
