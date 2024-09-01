using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2.0f;
    public float spawnDistance = 10.0f;   // Distance from the player to spawn enemies
    public float spawnRangeX = 5.0f;      // Range on the X-axis for random spawn positions
    public float spawnRangeZ = 5.0f;      // Range on the Z-axis for random spawn positions
    public float fixedYPosition = 5.0f;   // Fixed Y position for spawning enemies
    public Transform playerTransform;     // Reference to the player's transform

    private float nextSpawn = 0.0f;

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            SpawnEnemy();
        }

        // Debugging to verify the player's position is updated
        Debug.Log("Player's Current Position: " + playerTransform.position);
    }

    void SpawnEnemy()
    {
        // Generate a random position within the spawn range relative to the player's current position
        float randomX = Random.Range(-spawnRangeX, spawnRangeX) + playerTransform.position.x;
        float randomZ = Random.Range(-spawnRangeZ, spawnRangeZ) + playerTransform.position.z;

        // Use the fixed Y position for the enemy
        Vector3 spawnPosition = new Vector3(randomX, fixedYPosition, randomZ);

        // Debugging to verify spawn position calculation
        Debug.Log("Spawning enemy at position: " + spawnPosition);

        // Instantiate the enemy at the calculated position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
