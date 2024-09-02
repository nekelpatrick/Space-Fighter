using UnityEngine;

public enum WeaponType
{
 LASER,
 MISSILE
}

public class Weapon : MonoBehaviour
{
 public WeaponType weaponType;
 public int damage = 1;
 public int tier = 1;
 public string weaponClass = "Basic";

 public Transform firePoint;

 void Start()
 {
  // Ensure that firePoint is properly set up
  if (firePoint == null)
  {
   CreateFirePoint();
  }
 }

 void CreateFirePoint()
 {
  // Instantiate a new empty GameObject
  GameObject firePointObj = new GameObject("FirePoint");

  // Set its parent to this Weapon's transform
  firePointObj.transform.SetParent(transform);

  // Adjust its local position to be relative to the parent
  firePointObj.transform.localPosition = new Vector3(0, 1, 0);

  // Assign it to the firePoint variable for further use
  firePoint = firePointObj.transform;
 }

 public void Fire()
 {
  switch (weaponType)
  {
   case WeaponType.LASER:
    FireLaser();
    break;
   case WeaponType.MISSILE:
    FireMissile();
    break;
   default:
    Debug.LogError("Unknown weapon type.");
    break;
  }
 }

 private void FireLaser()
 {
  // Instantiate a laser bullet at the firePoint's position
  GameObject laserBullet = Instantiate(Resources.Load<GameObject>("Prefabs/LaserBullet"), firePoint.position, firePoint.rotation);
  LaserShotBehavior bullet = laserBullet.GetComponent<LaserShotBehavior>();
  if (bullet != null)
  {
   bullet.damage = damage;
  }
 }

 private void FireMissile()
 {
  // Instantiate a missile at the firePoint's position
  GameObject missile = Instantiate(Resources.Load<GameObject>("Prefabs/Missile"), firePoint.position, firePoint.rotation);
  LaserShotBehavior bullet = missile.GetComponent<LaserShotBehavior>();
  if (bullet != null)
  {
   bullet.damage = damage;
  }
 }
}
