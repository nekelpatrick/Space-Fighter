using UnityEngine;

/// <summary>
/// Makes the camera follow the player's X and Z position in real time, while maintaining a fixed Y position and ignoring rotation.
/// </summary>
public class CameraFollow : MonoBehaviour
{
 public Transform playerTransform;  // Reference to the player's transform
 public float fixedYPosition = 10.0f;  // The fixed Y position for the camera
 public Vector3 offset = new Vector3(0, 0, -10);  // Offset from the player's position on the XZ plane
 public float smoothSpeed = 0.125f;  // Speed of the camera's smooth transition

 private void LateUpdate()
 {
  if (playerTransform == null)
  {
   Debug.LogWarning("Player Transform is not assigned in CameraFollow script.");
   return;
  }

  // Calculate the target position based on the player's X and Z position, fixed Y position, and the offset
  Vector3 desiredPosition = new Vector3(playerTransform.position.x + offset.x, fixedYPosition, playerTransform.position.z + offset.z);

  // Smoothly interpolate between the camera's current position and the desired position
  Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

  // Update the camera's position to the smoothed position
  transform.position = smoothedPosition;

  // Optionally, look at the player (ignoring rotation)
  // transform.LookAt(new Vector3(playerTransform.position.x, fixedYPosition, playerTransform.position.z));
 }
}
