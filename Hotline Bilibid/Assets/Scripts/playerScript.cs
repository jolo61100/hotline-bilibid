using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour, iCharacterScript
{
    [Header("MOVEMENT")]
    [SerializeField] float _moveSpeed = 5.0f;
    Vector3 _movement;
    Vector2 _mousePosition;
    Rigidbody2D _rb;

    [Header("SHOOTING")]
    [SerializeField] Transform _firePoint;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _bulletForce = 15.0f;

    public Animator animator; 


    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animator.SetFloat("Vertical", _movement.y);
        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Magnitude", _movement.magnitude);


        _move();
        if (Input.GetButtonDown("Fire1"))
        {
            _shootBullet();
        }
    }

    private void FixedUpdate()
    {
        _moveLogic();
    }

    public void _move()
    {
        _movement = new Vector3 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

        transform.position = transform.position + _movement * Time.deltaTime;

        _movement.Normalize();
        _restrictMovement();
    }

    public void _moveLogic()
    {
        //_rb.MovePosition(_rb.position + (_movement * _moveSpeed * Time.fixedDeltaTime));
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       
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

    void _shootBullet()
    {
        GameObject _bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        Rigidbody2D _bulletRB = _bullet.GetComponent<Rigidbody2D>();
        _bulletRB.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);
    }
}