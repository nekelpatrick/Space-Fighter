using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the player's health, including damage feedback and health recovery.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
 public int maxHealth = 5;
 private int currentHealth;
 public Image healthBar; // Reference to UI element for health bar

 void Start()
 {
  currentHealth = maxHealth;
  UpdateHealthBar();
 }

 public void TakeDamage(int damage)
 {
  currentHealth -= damage;
  UpdateHealthBar();
  if (currentHealth <= 0)
  {
   HandleGameOver();
  }
 }

 public void RecoverHealth(int amount)
 {
  currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
  UpdateHealthBar();
 }

 private void UpdateHealthBar()
 {
  // Update the health bar UI
  healthBar.fillAmount = (float)currentHealth / maxHealth;
 }

 private void HandleGameOver()
 {
  // Handle game over (e.g., reload the scene)
 }
}
