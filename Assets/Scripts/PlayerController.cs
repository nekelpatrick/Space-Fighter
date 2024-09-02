using UnityEngine;

/// <summary>
/// Handles player movement based on input and rotates the ship to face the movement direction.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed = 50.0f;
    public float rotationSpeed = 15.0f; // Speed at which the ship rotates to face movement direction

    private Vector3 lastMovementDirection = Vector3.zero;
    private bool allowRotationControl = true; // Determines if the player can control rotation

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized; // Normalize to prevent diagonal speed boost

        // Update position based on movement input
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Only update rotation if there's movement input and rotation control is allowed
        if (allowRotationControl && movement.magnitude > 0.1f)
        {
            lastMovementDirection = movement;
            RotateTowardsDirection(movement);
        }
    }

    void RotateTowardsDirection(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SetRotationControl(bool allowControl)
    {
        allowRotationControl = allowControl;
    }

    public Vector3 GetLastMovementDirection()
    {
        return lastMovementDirection;
    }
}
