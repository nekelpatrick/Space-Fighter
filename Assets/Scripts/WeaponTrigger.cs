using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the interaction between the shooting trigger and the weapons.
/// </summary>
public class WeaponTrigger : MonoBehaviour
{
 public List<Weapon> equippedWeapons = new List<Weapon>(); // List to hold multiple weapons
 private AutoShoot autoShoot;

 void Start()
 {
  autoShoot = GetComponent<AutoShoot>();
  if (autoShoot == null)
  {
   Debug.LogError("AutoShoot component not found on the GameObject.");
  }
 }

 public void TriggerWeapons()
 {
  // Check if the nearest enemy is within firing range
  Transform target = autoShoot?.GetNearestEnemyInRange();
  if (target != null)
  {
   foreach (Weapon weapon in equippedWeapons)
   {
    if (weapon != null)
    {
     weapon.Fire();
    }
    else
    {
     Debug.LogWarning("A weapon is not properly equipped.");
    }
   }
  }
 }
}
