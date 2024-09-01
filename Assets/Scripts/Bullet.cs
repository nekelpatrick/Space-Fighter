using UnityEngine;

/// <summary>
/// Manages bullet behavior, including movement, lifetime, and collision detection.
/// </summary>
public class Bullet : MonoBehaviour
{
    public float speed = 20.0f;
    public float lifeTime = 2.0f;

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
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
