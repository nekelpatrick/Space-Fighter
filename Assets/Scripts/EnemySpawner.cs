using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the spawning of enemies at random positions relative to the player,
/// with dynamic difficulty adjustments.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float initialSpawnRate = 3.0f;  // Adjusted for less frequent spawning
    public float spawnRateDecrease = 0.05f; // Adjusted to reduce the rate of spawn frequency increase
    public float minSpawnRate = 1.0f;       // Minimum spawn rate for maximum difficulty
    public float spawnDistance = 20.0f;     // Increased distance for enemy spawning
    public Transform playerTransform;

    private float nextSpawn = 0.0f;
    private float currentSpawnRate;
    private HashSet<Transform> activeEnemies = new HashSet<Transform>();

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
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
        // Randomly spawn enemies at a greater distance from the player
        float randomAngle = Random.Range(0, 2 * Mathf.PI);
        float randomDistance = Random.Range(spawnDistance, spawnDistance + 10.0f); // Add variance to spawn distance
        float randomX = playerTransform.position.x + Mathf.Cos(randomAngle) * randomDistance;
        float randomZ = playerTransform.position.z + Mathf.Sin(randomAngle) * randomDistance;

        Vector3 spawnPosition = new Vector3(randomX, playerTransform.position.y, randomZ);

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(enemy.transform);

        // Notify AutoShoot of the new enemy
        playerTransform.GetComponent<AutoShoot>().AddEnemy(enemy.transform);
    }

    /// <summary>
    /// Removes an enemy from tracking when destroyed.
    /// </summary>
    /// <param name="enemy">The enemy to remove.</param>
    public void RemoveEnemy(Transform enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            playerTransform.GetComponent<AutoShoot>().RemoveEnemy(enemy);
        }
    }
}
