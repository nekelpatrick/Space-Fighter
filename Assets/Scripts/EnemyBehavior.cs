using UnityEngine;

/// <summary>
/// Base class for defining different enemy behaviors.
/// </summary>
public class EnemyBehavior : MonoBehaviour
{
 protected Transform player;

 protected virtual void Start()
 {
  player = GameObject.FindWithTag("Player").transform;
 }

 public virtual void PerformAction()
 {
  // Default enemy behavior (e.g., move towards the player)
 }
}

public class FastEnemyBehavior : EnemyBehavior
{
 public float speed = 10.0f;

 public override void PerformAction()
 {
  // Custom behavior for fast enemies
  Vector3 direction = (player.position - transform.position).normalized;
  transform.Translate(direction * speed * Time.deltaTime, Space.World);
 }
}

public class HeavyEnemyBehavior : EnemyBehavior
{
 public float speed = 3.0f;

 public override void PerformAction()
 {
  // Custom behavior for heavy enemies
  Vector3 direction = (player.position - transform.position).normalized;
  transform.Translate(direction * speed * Time.deltaTime, Space.World);
 }
}
