using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunScript : MonoBehaviour
{
    gunData _data;
    [SerializeField] Transform _firePoint;
    [SerializeField] playerScript _player;
    private float _timeBtwShots;

    private void Update()
    {
        _data = _player.returnData();
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (_timeBtwShots <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject _bullet = Instantiate(_data._bulletPrefab, _firePoint.position, transform.rotation);
                Rigidbody2D _bulletRB = _bullet.GetComponent<Rigidbody2D>();
                _bulletRB.AddForce(_firePoint.up * _data._bulletForce, ForceMode2D.Impulse);
                _timeBtwShots = _data._startTimeBtwShots;
            }
        }
        else
        {
            _timeBtwShots -= Time.deltaTime;
        }
    }

    //     GameObject _bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    //     Rigidbody2D _bulletRB = _bullet.GetComponent<Rigidbody2D>();
    //     _bulletRB.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);
}
