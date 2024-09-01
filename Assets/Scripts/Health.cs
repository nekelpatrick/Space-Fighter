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
 }

 /// <summary>
 /// Reduces health by a specified amount.
 /// </summary>
 /// <param name="damage">The amount of damage to apply.</param>
 public void TakeDamage(int damage)
 {
  currentHealth -= damage;
  if (currentHealth <= 0)
  {
   Die();
  }
 }

 /// <summary>
 /// Handles the entity's death, ensuring it is properly removed from all tracking systems.
 /// </summary>
 private void Die()
 {
  if (enemySpawner != null)
  {
   enemySpawner.RemoveEnemy(transform);
  }
  Destroy(gameObject);
 }
}
