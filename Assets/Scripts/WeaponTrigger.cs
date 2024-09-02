using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the interaction between the shooting trigger and the weapons.
/// </summary>
public class WeaponTrigger : MonoBehaviour
{
 public List<Weapon> equippedWeapons = new List<Weapon>(); // List to hold multiple weapons

 public void TriggerWeapons()
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
