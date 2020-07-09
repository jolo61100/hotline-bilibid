using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform[] firePoints;
    [SerializeField] float nextFire;
    [SerializeField] float fireRate;
    void Update()
    {
        shoot();
    }

    void FixedUpdate()
    {
        Aim();
    }

    void Aim()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

    void shoot()
    {
        if (Input.GetButton("Fire1") && nextFire <= Time.time)
        {
            foreach (Transform firePoint in firePoints)
            {
                nextFire = Time.time + fireRate;
                Instantiate(projectile, firePoint.position, firePoint.rotation);//Instantiate makes the bullets spawn at the firepoint position and rotation
            }
        }
    }
}
