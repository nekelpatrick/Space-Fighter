using UnityEngine;

/// <summary>
/// Manages laser bullet behavior, including movement, lifetime, and collision detection.
/// </summary>
public class LaserShotBehavior : MonoBehaviour
{
    public float speed = 50.0f;
    public float lifeTime = 2.0f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy after lifetime ends
    }

    void Update()
    {
        // Move the laser forward at a high speed
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy laser after hitting an enemy
        }
    }
}
