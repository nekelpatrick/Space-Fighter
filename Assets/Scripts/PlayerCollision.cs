using UnityEngine;

/// <summary>
/// Handles player collisions with enemies, including health management.
/// </summary>
public class PlayerCollision : MonoBehaviour
{
    private PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            playerHealth.TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}
