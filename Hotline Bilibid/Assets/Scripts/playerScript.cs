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

    AudioSource audioSrc;
    bool isMoving = false;

    // [Header("SHOOTING")]
    // [SerializeField] Transform _firePoint;
    // [SerializeField] GameObject _bulletPrefab;
    // [SerializeField] float _bulletForce = 15.0f;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        _rb = this.GetComponent<Rigidbody2D>();
        audioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gameplayScript._isAlive)
        {
            animator.SetFloat("Vertical", _movement.y);
            animator.SetFloat("Horizontal", _movement.x);
            animator.SetFloat("Magnitude", _movement.magnitude);
            _move();
        }
        else
        {
            _movement = new Vector2(0, 0);
        }
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

        if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
        }
        else
        {
            audioSrc.Stop();
        }


        _movement.Normalize();
        _restrictMovement();
    }

    public void _moveLogic()
    {
        _rb.MovePosition(_rb.position + _movement * _moveSpeed * Time.fixedDeltaTime);
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


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<AudioManager>().Play("Hurt");
            healthCounter._currentHealth -= 1;
            Debug.Log("oof owie" + healthCounter._currentHealth);
        }
        if (healthCounter._currentHealth <= 0)
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
        }
    }
}