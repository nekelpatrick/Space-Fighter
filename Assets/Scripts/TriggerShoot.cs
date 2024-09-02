using UnityEngine;

/// <summary>
/// Triggers weapon firing based on specific conditions (e.g., timing).
/// </summary>
public class TriggerShoot : MonoBehaviour
{
 public float fireRate = 0.5f;
 private float nextFire = 0.0f;
 private WeaponTrigger weaponTrigger;

 void Start()
 {
  weaponTrigger = GetComponent<WeaponTrigger>();
  if (weaponTrigger == null)
  {
   Debug.LogError("WeaponTrigger component not found on the GameObject.");
  }
 }

 void Update()
 {
  if (Time.time > nextFire)
  {
   nextFire = Time.time + fireRate;
   weaponTrigger?.TriggerWeapons(); // Trigger all weapons
  }
 }
}
