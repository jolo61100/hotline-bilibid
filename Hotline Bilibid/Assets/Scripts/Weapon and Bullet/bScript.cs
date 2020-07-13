using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bScript : MonoBehaviour
{
    public float lifeTime;
    public float distance;
    public float damage;
    public float speed;
    public GameObject destroyEffect;
    public LayerMask whatIsSolid;

    void Start()
    {
        Invoke("destroyProjectile", lifeTime);//When spawned it will have this function
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            gameplayScript._playerScore++;
            Destroy(collision.gameObject);
            destroyProjectile();
        }

        if (collision.collider != null)//doesn't work with wall
        {
            Debug.Log(collision.gameObject);
            destroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
    }
    void rayCastTest()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance);//Raycast is like a laser beam
        transform.Translate(Vector2.up * speed * Time.deltaTime);//make projectile fly
        if (hitInfo.collider != null && hitInfo.collider != hitInfo.collider.CompareTag("Player"))//Checks if it collided with something
        {
            if (hitInfo.collider.CompareTag("Enemy"))//checks the tag if it's an enemy
            {
                Debug.Log("test");
            }
        }
    }

    void Translation()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);//makes the projectile move
    }
    void Update()
    {
        //rayCastTest();
        Translation();

    }
    void destroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
