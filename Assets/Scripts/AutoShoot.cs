using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the automatic shooting mechanism for the player character.
/// Continuously aims at the nearest enemy and shoots.
/// </summary>
public class AutoShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform laserOutArea;
    public float fireRate = 0.5f;
    public float detectionRange = 15.0f; // Range within which enemies can be targeted

    private float nextFire = 0.0f;
    private Transform nearestEnemy;
    private HashSet<Transform> enemies = new HashSet<Transform>(); // Tracks active enemies

    void Update()
    {
        nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            AimAtEnemy(nearestEnemy);
        }

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, laserOutArea.position, laserOutArea.rotation);
    }

    /// <summary>
    /// Aims the shooting area towards the nearest enemy.
    /// </summary>
    /// <param name="target">The nearest enemy to target.</param>
    void AimAtEnemy(Transform target)
    {
        Vector3 direction = target.position - laserOutArea.position;
        laserOutArea.rotation = Quaternion.LookRotation(direction);
    }

    /// <summary>
    /// Finds the nearest enemy within a certain range.
    /// </summary>
    /// <returns>The Transform of the nearest enemy, or null if none found.</returns>
    Transform FindNearestEnemy()
    {
        Transform closestEnemy = null;
        float closestDistance = detectionRange;

        foreach (Transform enemy in enemies)
        {
            if (enemy == null) continue; // Skip destroyed enemies

            float distance = Vector3.Distance(transform.position, enemy.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
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
