using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour, iCharacterScript
{
    [SerializeField] float _moveSpeed = 5.0f;
    Vector2 _movement;
    Transform _target;
    Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Physics2D.queriesStartInColliders = false;
    }

    private void FixedUpdate()
    {
        _moveLogic();
    }

    public void _move()
    {
        //cannot remove due to interface
    }

    public void _moveLogic()
    {
        _movement = Vector2.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);
        _rb.MovePosition(_movement);
        Vector2 lookDirection = (Vector2)_target.position - _rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
        _restrictMovement();
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

    public void _shootBullet()
    {

    }

    

}
