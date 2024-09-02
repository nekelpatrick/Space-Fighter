using UnityEngine;

/// <summary>
/// Manages the health of an entity, such as an enemy, ensuring proper destruction upon death.
/// </summary>
public class Health : MonoBehaviour
{
 public int maxHealth = 3;
 private int currentHealth;
 private EnemySpawner enemySpawner;

 void Start()
 {
  currentHealth = maxHealth;
  enemySpawner = FindObjectOfType<EnemySpawner>();
  Debug.Log($"{gameObject.name} spawned with {currentHealth} health.");
 }

 public void TakeDamage(int damage)
 {
  currentHealth -= damage;
  Debug.Log($"{gameObject.name} took {damage} damage, remaining health: {currentHealth}");

  if (currentHealth <= 0)
  {
   Die();
  }
 }

 private void Die()
 {
  Debug.Log($"{gameObject.name} has died.");

  if (enemySpawner != null)
  {
   enemySpawner.RemoveEnemy(transform);
  }

  Destroy(gameObject);
 }
}