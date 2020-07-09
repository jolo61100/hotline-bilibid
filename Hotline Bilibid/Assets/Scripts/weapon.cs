using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour 
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] int bulletCount;
    [SerializeField] float nextFire;
    [SerializeField] float fireRate;
    [SerializeField] bool autoFire; 

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
        if (autoFire == true)//auto
        {
            if (Input.GetMouseButton(0) && nextFire <= Time.time)
            {
                if (bulletCount > 1)
                {
                    for (int shots = 0; shots != bulletCount; shots++)//multi bullet gun
                    {
                        nextFire = Time.time + fireRate;
                        float rand = Random.Range(-10f, 10f);           //Randomize value first
                        firePoint.transform.Rotate(0, 0, 0 + rand);     //Set the randomize value to z (Angle of Barrel)
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                        firePoint.transform.Rotate(0, 0, 0 - rand);     //Resets the barrel's position back to normal
                    }
                }
                else // single bullet
                {
                    nextFire = Time.time + fireRate;
                    firePoint.transform.Rotate(0, 0, 0);
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                }
            }
        }

        else//semi auto
        {
            if (Input.GetMouseButtonDown(0) && nextFire <= Time.time)
            {
                if (bulletCount > 1)
                {
                    for (int shots = 0; shots != bulletCount; shots++)
                    {
                        nextFire = Time.time + fireRate;
                        float rand = Random.Range(-10f, 10f);           //Randomize value first
                        firePoint.transform.Rotate(0, 0, 0 + rand);     //Set the randomize value to z (Angle of Barrel)
                        Instantiate(bullet, firePoint.position, firePoint.rotation);
                        firePoint.transform.Rotate(0, 0, 0 - rand);     //Resets the barrel's position back to normal
                    }
                }
                else
                {
                    nextFire = Time.time + fireRate;
                    firePoint.transform.Rotate(0, 0, 0);
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                }
            }
        }
    }          
}