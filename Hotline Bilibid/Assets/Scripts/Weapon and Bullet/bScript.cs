using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bScript : MonoBehaviour
{
    public float lifeTime;
    public float maxLifeTime;
    public float distance;
    public float damage;
    public float speed;
    public GameObject destroyEffect;
    public LayerMask whatIsSolid;

    void Start()
    {
        Invoke("destroyProjectile", lifeTime);//When spawned it will have this function
    }
    private void OnEnable()
    {
        lifeTime = 0f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            FindObjectOfType<AudioManager>().Play("EnemyDeath");
           // gameplayScript._playerScore++;
            collision.gameObject.SetActive(false);
            EnemyPool.Instance.ReturnToPool(collision.gameObject);
            Debug.Log("!" + collision.gameObject);
            TESTPOOL.Instance.ReturnToPool(this.gameObject);
        }

        if (collision.collider.CompareTag("Wall"))
        {
            Debug.Log(collision.gameObject);
            TESTPOOL.Instance.ReturnToPool(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
    }
    void Translation()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);//makes the projectile move
    }
    void Update()
    {
        Translation();
        destroyProjectile();
    }
    void destroyProjectile()
    {
        lifeTime += Time.deltaTime;
        if(lifeTime == maxLifeTime)
        {
            TESTPOOL.Instance.ReturnToPool(this.gameObject);
        }
    }
}