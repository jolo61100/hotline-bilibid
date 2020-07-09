using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour, iCharacterScript
{
    [Header("MOVEMENT")]
    [SerializeField] float _moveSpeed = 5.0f;
    Vector2 _movement;
    Vector2 _mousePosition;
    Rigidbody2D _rb;

    [Header("SHOOTING")]
    [SerializeField] Transform _firePoint;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _bulletForce = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _move();
    }

    private void FixedUpdate()
    {
        _moveLogic();
        if (Input.GetButton("Fire1"))
        {
            //_shootBullet();
        }
    }

    public void _move()
    {
        _movement = new Vector2
            (Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical"));

        _movement.Normalize();
        _restrictMovement();
    }

    public void _moveLogic()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = _mousePosition - _rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
    }

    public void _restrictMovement()
    {
        Vector3 upperRightCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 lowerLeftCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));

        float playerWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float playerHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

        float xVal = Mathf.Clamp(transform.position.x, lowerLeftCorner.x + playerWidth, upperRightCorner.x - playerWidth);
        float yVal = Mathf.Clamp(transform.position.y, lowerLeftCorner.y + playerHeight, upperRightCorner.y - playerHeight);

        transform.position = new Vector3(xVal, yVal, 0);
    }

    /*public void _shootBullet()//fix
    {
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);

        Rigidbody2D _bulletRB = bullet.GetComponent<Rigidbody2D>();

        _bulletRB.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);

    }*/

}