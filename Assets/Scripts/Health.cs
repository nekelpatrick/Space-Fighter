using UnityEngine;

/// <summary>
/// Manages the health of an entity, such as an enemy.
/// </summary>
public class Health : MonoBehaviour
{
 public int maxHealth = 3;
 private int currentHealth;

 void Start()
 {
  currentHealth = maxHealth;
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
 /// Handles the entity's death.
 /// </summary>
 private void Die()
 {
  Destroy(gameObject);
 }
}
