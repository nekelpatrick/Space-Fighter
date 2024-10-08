using UnityEngine;

/// <summary>
/// Base class for defining different enemy behaviors.
/// </summary>
public class EnemyBehavior : MonoBehaviour
{
 protected Transform player;
 protected HordeManager hordeManager;

 protected virtual void Start()
 {
  player = GameObject.FindWithTag("Player").transform;
  hordeManager = FindObjectOfType<HordeManager>();
  if (hordeManager != null)
  {
   hordeManager.AddEnemy(transform);
  }
 }

 public virtual void PerformAction()
 {
  // Default behavior is overridden by subclasses.
 }

 public void MoveAsHorde(Vector3 direction)
 {
  // Default movement as part of the horde
  transform.Translate(direction * Time.deltaTime, Space.World);
 }

 void OnDestroy()
 {
  if (hordeManager != null)
  {
   hordeManager.RemoveEnemy(transform);
  }
 }
}

public class FastEnemyBehavior : EnemyBehavior
{
 public float baseSpeed = 18.0f;

 public override void PerformAction()
 {
  float speed = baseSpeed * hordeManager.GetSpeedMultiplier();
  MoveAsHorde((player.position - transform.position).normalized * speed);
 }
}

public class HeavyEnemyBehavior : EnemyBehavior
{
 public float baseSpeed = 10.0f;

 public override void PerformAction()
 {
  float speed = baseSpeed * hordeManager.GetSpeedMultiplier();
  MoveAsHorde((player.position - transform.position).normalized * speed);
 }
}
