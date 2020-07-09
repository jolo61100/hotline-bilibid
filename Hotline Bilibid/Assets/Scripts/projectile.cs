using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float lifeTime;
    public float distance;
    public float damage;
    public float speed;
    public GameObject destroyEffect;
    public LayerMask whatIsSolid;//no walls yet

    void Start()
    {
        Invoke("destroyProjectile", lifeTime);//When spawned it will have this function
    }

    /*
    Make an OnColliderEnter2D function for impact hits
    Problem: No idea how to make the sendMessage get component shit to make work on a OnColliderEnter2D function
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("IT HURTS");
            destroyProjectile();
        }

        if (collision.collider.CompareTag("Wall"))//doesn't work with wall
        {
            Debug.Log("WALLED");
            destroyProjectile();
        }
    }

    void Translation()
    {
        
        transform.Translate(Vector2.up * speed * Time.deltaTime);//makes the projectile move
    }
    

    void Update()
    {
        Translation();
        //RayCast();
    }

    void RayCast()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance);//Raycast is like a laser beam
        if (hitInfo.collider != null && hitInfo.collider != hitInfo.collider.CompareTag("Player"))
        {//Checks if it collided with something
            if (hitInfo.collider.CompareTag("Enemy"))
            {//checks the tag if it's an enemy
                //hitInfo.collider.GetComponent<Enemy>().takeDamage(damage);//gets the damage value from this object then transfer to the one getting collided with
            }
            GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);//Quaternion.Identity is basically the last position it was in
            Destroy(effect, 0.5f);
            destroyProjectile();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);//make projectile fly
    }


    void destroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
