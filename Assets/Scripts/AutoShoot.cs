using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles automatic shooting for the player, rotating the spaceship to aim at the nearest enemy.
/// </summary>
public class AutoShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform laserOutArea;
    public float fireRate = 0.5f;
    public float detectionRange = 15.0f; // Range within which enemies can be targeted
    public float rotationSpeed = 5.0f;   // Speed at which the spaceship rotates towards the enemy
    public float firingAngleThreshold = 5.0f; // Angle within which the spaceship can fire at the enemy

    private float nextFire = 0.0f;
    private Transform nearestEnemy;
    private HashSet<Transform> enemies = new HashSet<Transform>(); // Tracks active enemies

    void Update()
    {
        nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            AimAtEnemy(nearestEnemy);
            if (IsAimedAtEnemy(nearestEnemy) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }
        }
    }

    /// <summary>
    /// Instantiates a bullet and shoots it towards the current target.
    /// </summary>
    void Shoot()
    {
        Instantiate(bulletPrefab, laserOutArea.position, laserOutArea.rotation);
    }

    /// <summary>
    /// Aims the spaceship towards the nearest enemy.
    /// </summary>
    /// <param name="target">The nearest enemy to target.</param>
    void AimAtEnemy(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed); // Rotate the spaceship itself
    }

    /// <summary>
    /// Checks if the spaceship is sufficiently aimed at the enemy to fire.
    /// </summary>
    /// <param name="target">The nearest enemy to target.</param>
    /// <returns>True if the spaceship is aimed at the enemy, false otherwise.</returns>
    bool IsAimedAtEnemy(Transform target)
    {
        Vector3 directionToEnemy = (target.position - transform.position).normalized;
        float angleToEnemy = Vector3.Angle(transform.forward, directionToEnemy);
        return angleToEnemy < firingAngleThreshold;
    }

    /// <summary>
    /// Finds the nearest enemy within a certain range.
    /// </summary>
    /// <returns>The Transform of the nearest enemy, or null if none found.</returns>
    Transform FindNearestEnemy()
    {
        Transform closestEnemy = null;
        float closestDistanceSqr = detectionRange * detectionRange; // Using squared distance for performance

        foreach (Transform enemy in enemies)
        {
            if (enemy == null) continue; // Skip destroyed enemies

            float distanceSqr = (enemy.position - transform.position).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    /// <summary>
    /// Adds an enemy to the tracking set.
    /// </summary>
    /// <param name="enemy">The enemy to track.</param>
    public void AddEnemy(Transform enemy)
    {
        enemies.Add(enemy);
    }

    /// <summary>
    /// Removes an enemy from the tracking set.
    /// </summary>
    /// <param name="enemy">The enemy to stop tracking.</param>
    public void RemoveEnemy(Transform enemy)
    {
        enemies.Remove(enemy);
    }
}
