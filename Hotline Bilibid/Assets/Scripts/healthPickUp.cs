using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<AudioManager>().Play("Health");
            healthCounter._currentHealth += 1;
            if (healthCounter._currentHealth > healthCounter._maxHealth) {
                healthCounter._currentHealth = healthCounter._maxHealth;
            }
            this.gameObject.SetActive(false);
            Debug.Log("I need Healing: " + healthCounter._currentHealth);
        }
    }
}
