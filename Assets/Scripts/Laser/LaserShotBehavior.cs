using UnityEngine;

/// <summary>
/// Manages bullet behavior, including movement, lifetime, and collision detection.
/// </summary>
public class LaserShotBehavior : MonoBehaviour
{
    public float speed = 50.0f;
    public float lifeTime = 2.0f;
    public int damage = 1;
    public bool isLaser = false; // Set this to true if the projectile should behave like a laser

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Adjust speed or behavior if it's a laser
        if (isLaser)
        {
            transform.position += transform.forward * Time.deltaTime * 1000f; // Laser movement
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // Regular shot movement
        }
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
            Destroy(gameObject);
        }
    }
}
