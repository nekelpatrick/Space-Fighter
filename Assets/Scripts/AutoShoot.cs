using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles automatic enemy tracking and spaceship rotation towards the nearest enemy.
/// </summary>
public class AutoShoot : MonoBehaviour
{
    public float detectionRange = 60.0f;
    public float rotationSpeed = 15f;
    public float firingAngleThreshold = 15f;

    private Transform nearestEnemy;
    private HashSet<Transform> enemies = new HashSet<Transform>(); // Tracks active enemies

    void Update()
    {
        nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null)
        {
            AimAtEnemy(nearestEnemy);
        }
    }

    void AimAtEnemy(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    Transform FindNearestEnemy()
    {
        Transform closestEnemy = null;
        float closestDistanceSqr = detectionRange * detectionRange;

        foreach (Transform enemy in enemies)
        {
            if (enemy == null) continue;

            float distanceSqr = (enemy.position - transform.position).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    public void AddEnemy(Transform enemy)
    {
        if (enemy != null)
        {
            enemies.Add(enemy);
        }
    }

    public void RemoveEnemy(Transform enemy)
    {
        enemies.Remove(enemy);
    }
}
