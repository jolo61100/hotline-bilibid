using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class weaponScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] int bulletCount;
    [SerializeField] float nextFire;
    [SerializeField] float fireRate;
    [SerializeField] bool autoFire;
    [SerializeField] float damage;
    [SerializeField] float bulletSpeed;


    [SerializeField] AudioManager _audio;

    public AudioClip impact;
    AudioSource audioSource;

    void Start()
    {
        _audio = FindObjectOfType<AudioManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        shoot();
    }

    void FixedUpdate()
    {
        //Aim();
    }

    void Aim() // direction of where to shoot
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

    void poolShoot()
    {
        var shot = TESTPOOL.Instance.Get();
        shot.transform.rotation = firePoint.rotation;
        shot.transform.position = firePoint.position;
        shot.gameObject.SetActive(true);
        Debug.Log("pooled");
    }


    void shoot()
    {
        if (autoFire == true)//auto
        {
            if (Input.GetMouseButton(0) && nextFire <= Time.time)
            {
                if (bulletCount > 1)
                {
                    //_audio.Play("MachineGun");
                    // audioSource.PlayOneShot(impact, 1F); //PlayMachineGun SFX
                    Debug.Log("HOLD");
                    for (int shots = 0; shots != bulletCount; shots++)//multi bullet gun
                    {
                        nextFire = Time.time + fireRate;
                        float rand = Random.Range(-7f, 7f);           //Randomize value first
                        firePoint.transform.Rotate(0, 0, 0 + rand);     //Set the randomize value to z (Angle of Barrel)
                        poolShoot();
                        firePoint.transform.Rotate(0, 0, 0 - rand);     //Resets the barrel's position back to normal
                    }
                }
                else // single bullet
                {
                    _audio.Play("MachineGun"); //for some reason ito ang nagpeplay ng machinegun
                    nextFire = Time.time + fireRate;
                    firePoint.transform.Rotate(0, 0, 0);
                    poolShoot();
                }
            }
        }

        else//semi auto
        {
            if (Input.GetMouseButtonDown(0) && nextFire <= Time.time)
            {

                if (bulletCount > 1)
                {
                    _audio.Play("Shotgun");
                    for (int shots = 0; shots != bulletCount; shots++)
                    {
                        nextFire = Time.time + fireRate;
                        float rand = Random.Range(-10f, 10f);           //Randomize value first
                        firePoint.transform.Rotate(0, 0, 0 + rand);     //Set the randomize value to z (Angle of Barrel)
                        poolShoot();
                        firePoint.transform.Rotate(0, 0, 0 - rand);     //Resets the barrel's position back to normal
                    }
                }
                else
                {
                    _audio.Play("Pistol");
                    nextFire = Time.time + fireRate;
                    firePoint.transform.Rotate(0, 0, 0);
                    poolShoot();
                }
            }
        }
    }
}