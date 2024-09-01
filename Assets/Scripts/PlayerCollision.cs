using UnityEngine;

/// <summary>
/// Handles player collisions with enemies, including health management.
/// </summary>
public class PlayerCollision : MonoBehaviour
{
    public int playerHealth = 3;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerHealth--;
            Destroy(other.gameObject);

            if (playerHealth <= 0)
            {
                HandleGameOver();
            }
        }
    }

    /// <summary>
    /// Handles game over logic, such as reloading the scene or displaying a game over screen.
    /// </summary>
    void HandleGameOver()
    {
        // Implement game over logic here, such as reloading the scene
    }
}
