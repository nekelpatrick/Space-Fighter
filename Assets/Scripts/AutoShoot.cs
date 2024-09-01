using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform LaserOutArea;
    public float fireRate = 0.5f;

    private float nextFire = 0.0f;

    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, LaserOutArea.position, LaserOutArea.rotation);
    }
}
