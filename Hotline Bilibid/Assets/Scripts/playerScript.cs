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
    //[SerializeField] gunData _data;
    private Animator animator;

    // [Header("SHOOTING")]
    // [SerializeField] Transform _firePoint;
    // [SerializeField] GameObject _bulletPrefab;
    // [SerializeField] float _bulletForce = 15.0f;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        _rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        animator.SetFloat("Vertical", _movement.y);
        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Magnitude", _movement.magnitude);

        _move();
        // if (Input.GetButtonDown("Fire1"))
        // {
        //     _shootBullet();
        // }
    }

    private void FixedUpdate()
    {
        _moveLogic();
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
        // _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Vector2 lookDirection = _mousePosition - _rb.position;
        // float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        // _rb.rotation = angle;
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

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Gun"))
    //     {
    //         _data = other.gameObject.GetComponent<gunPickUp>().returnData();
    //         Debug.Log(_data._name);
    //         Destroy(other.gameObject);
    //     }
    // }

    // public gunData returnData()
    // {
    //     return _data;
    // }

    // void _shootBullet()
    // {
    //     GameObject _bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    //     Rigidbody2D _bulletRB = _bullet.GetComponent<Rigidbody2D>();
    //     _bulletRB.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);
    // }
}