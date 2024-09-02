using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the horde behavior of enemies, coordinating their movement towards the player.
/// </summary>
public class HordeManager : MonoBehaviour
{
 public List<Transform> enemies = new List<Transform>(); // List of enemies in the horde
 public float cohesionFactor = 1.0f; // How strongly the enemies group together
 public float separationDistance = 2.0f; // Minimum distance between enemies
 public float alignmentFactor = 1.0f; // How strongly the enemies align their direction with the group
 public float attackSpeedMultiplier = 1.5f; // Speed multiplier when the horde attacks

 private Transform player;

 void Start()
 {
  player = GameObject.FindWithTag("Player").transform;
 }

 void Update()
 {
  foreach (Transform enemy in enemies)
  {
   if (enemy != null)
   {
    Vector3 moveDirection = CalculateHordeMovement(enemy);
    enemy.GetComponent<EnemyBehavior>().MoveAsHorde(moveDirection);
   }
  }
 }

 Vector3 CalculateHordeMovement(Transform enemy)
 {
  Vector3 cohesion = CalculateCohesion(enemy);
  Vector3 separation = CalculateSeparation(enemy);
  Vector3 alignment = CalculateAlignment(enemy);
  Vector3 targetDirection = (player.position - enemy.position).normalized;

  // Combine all forces for the final movement direction
  return (cohesion * cohesionFactor + separation + alignment * alignmentFactor + targetDirection).normalized;
 }

 Vector3 CalculateCohesion(Transform enemy)
 {
  Vector3 centerOfMass = Vector3.zero;
  int neighborCount = 0;

  foreach (Transform other in enemies)
  {
   if (other != enemy)
   {
    centerOfMass += other.position;
    neighborCount++;
   }
  }

  if (neighborCount == 0)
   return Vector3.zero;

  centerOfMass /= neighborCount;
  return (centerOfMass - enemy.position).normalized;
 }

 Vector3 CalculateSeparation(Transform enemy)
 {
  Vector3 separation = Vector3.zero;

  foreach (Transform other in enemies)
  {
   if (other != enemy)
   {
    float distance = Vector3.Distance(enemy.position, other.position);
    if (distance < separationDistance)
    {
     separation += (enemy.position - other.position).normalized / distance;
    }
   }
  }

  return separation;
 }

 Vector3 CalculateAlignment(Transform enemy)
 {
  Vector3 averageDirection = Vector3.zero;
  int neighborCount = 0;

  foreach (Transform other in enemies)
  {
   if (other != enemy)
   {
    averageDirection += other.forward;
    neighborCount++;
   }
  }

  if (neighborCount == 0)
   return enemy.forward;

  averageDirection /= neighborCount;
  return averageDirection.normalized;
 }

 public void AddEnemy(Transform enemy)
 {
  if (!enemies.Contains(enemy))
  {
   enemies.Add(enemy);
  }
 }

 public void RemoveEnemy(Transform enemy)
 {
  enemies.Remove(enemy);
 }
}
