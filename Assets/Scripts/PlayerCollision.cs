using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
                // Handle game over (e.g., reload scene)
            }
        }
    }
}
