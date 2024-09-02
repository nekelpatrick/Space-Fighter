using UnityEngine;

/// <summary>
/// Handles the interaction between the shooting trigger and the weapon.
/// </summary>
public class WeaponTrigger : MonoBehaviour
{
 public Weapon equippedWeapon;

 public void TriggerWeapon()
 {
  if (equippedWeapon != null)
  {
   equippedWeapon.Fire();
  }
  else
  {
   Debug.LogWarning("No weapon equipped.");
  }
 }
}
