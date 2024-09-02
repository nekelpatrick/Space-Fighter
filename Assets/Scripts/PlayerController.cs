using UnityEngine;

/// <summary>
/// Handles player movement based on input.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed = 25.0f;

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized; // Normalize to prevent diagonal speed boost
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
}
