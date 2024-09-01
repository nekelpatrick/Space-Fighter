using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the spawning of enemies at random positions relative to the player.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2.0f;
    public float spawnRangeX = 5.0f;
    public float spawnRangeZ = 5.0f;
    public float fixedYPosition = 5.0f;
    public Transform playerTransform;

    private float nextSpawn = 0.0f;
    private HashSet<Transform> activeEnemies = new HashSet<Transform>();

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(-spawnRangeX, spawnRangeX) + playerTransform.position.x;
        float randomZ = Random.Range(-spawnRangeZ, spawnRangeZ) + playerTransform.position.z;

        Vector3 spawnPosition = new Vector3(randomX, fixedYPosition, randomZ);

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
