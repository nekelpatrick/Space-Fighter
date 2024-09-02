using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles automatic enemy tracking and ship rotation when within firing range.
/// </summary>
public class AutoShoot : MonoBehaviour
{
    public float detectionRange = 60.0f; // Range for tracking enemies
    public float firingRange = 30.0f;    // Range within which the player can shoot at enemies
    public float rotationSpeed = 15f;    // Speed of rotating towards the enemy

    private Transform nearestEnemy;
    private HashSet<Transform> enemies = new HashSet<Transform>(); // Tracks active enemies
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController script not found on the Player object.");
        }
    }

    void Update()
    {
        nearestEnemy = FindNearestEnemy();

        // If an enemy is within firing range, take control of the ship's rotation
        if (nearestEnemy != null)
        {
            float distanceSqr = (nearestEnemy.position - transform.position).sqrMagnitude;
            if (distanceSqr <= firingRange * firingRange)
            {
                // Disable player rotation control
                playerController.SetRotationControl(false);

                // Aim and shoot at the enemy
                AimAtEnemy(nearestEnemy);
            }
            else
            {
                // Re-enable player rotation control if out of firing range
                playerController.SetRotationControl(true);
            }
        }
        else
        {
            // Re-enable player rotation control if no enemy is detected
            playerController.SetRotationControl(true);
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

    public Transform GetNearestEnemyInRange()
    {
        if (nearestEnemy == null)
            return null;

        float distanceSqr = (nearestEnemy.position - transform.position).sqrMagnitude;
        if (distanceSqr <= firingRange * firingRange)
        {
            return nearestEnemy;
        }

        return null;
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
