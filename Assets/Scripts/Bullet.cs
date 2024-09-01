using UnityEngine;

/// <summary>
/// Manages bullet behavior, including movement, lifetime, and collision detection.
/// </summary>
public class Bullet : MonoBehaviour
{
    public float speed = 20.0f;
    public float lifeTime = 2.0f;
    public int damage = 1; // Amount of damage the bullet deals

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
