using UnityEngine;

public enum WeaponType
{
  LASER,
  MISSILE
}

/// <summary>
/// Represents a modular weapon that can be equipped by the player.
/// </summary>
public class Weapon : MonoBehaviour
{
  public WeaponType weaponType;
  public int damage = 1;
  public int tier = 1;
  public string weaponClass = "Basic";

  public Transform firePoint; // The point from which the weapon fires

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
    // Instantiate a new empty GameObject as the fire point
    GameObject firePointObj = new GameObject("FirePoint");
    firePointObj.transform.SetParent(transform);
    firePointObj.transform.localPosition = new Vector3(0, 1, 0);
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
    // Load and instantiate the laser bullet at the fire point
    GameObject laserBullet = Instantiate(Resources.Load<GameObject>("Prefabs/LaserBullet"), firePoint.position, firePoint.rotation);
    LaserShotBehavior bullet = laserBullet.GetComponent<LaserShotBehavior>();
    if (bullet != null)
    {
      bullet.damage = damage;
    }
  }

  private void FireMissile()
  {
    // Load and instantiate the missile at the fire point
    GameObject missile = Instantiate(Resources.Load<GameObject>("Prefabs/Missile"), firePoint.position, firePoint.rotation);
    LaserShotBehavior bullet = missile.GetComponent<LaserShotBehavior>();
    if (bullet != null)
    {
      bullet.damage = damage;
    }
  }
}
