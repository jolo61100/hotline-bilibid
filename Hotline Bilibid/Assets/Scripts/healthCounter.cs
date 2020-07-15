using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthCounter : MonoBehaviour
{
    [Header("Health Stuff")]
    public static int _maxHealth = 3;
    [SerializeField] int _noOfLives;

    [SerializeField] Image[] _lives;
    [SerializeField] Sprite _emptyLife;
    [SerializeField] Sprite _fullLife;

    public static int _currentHealth;

    private void Start()
    {

        _currentHealth = _maxHealth;

    }

    private void Update()
    {

        HealthChecker();
    }

    void HealthChecker()
    {
        if (_maxHealth < _noOfLives)
        {
            //FindObjectOfType<AudioManager>().Play("PlayerDeath");
            _noOfLives = _maxHealth;
        }

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
        }


        for (int i = 0; i < _lives.Length; i++)
        {
            if (i < _currentHealth)
            {
                _lives[i].sprite = _fullLife;
            }
            else
            {
                _lives[i].sprite = _emptyLife;
            }

            if (i < _noOfLives)
            {
                _lives[i].enabled = true;
            }
            else
            {
                _lives[i].enabled = false;
            }
        }


    }

}
