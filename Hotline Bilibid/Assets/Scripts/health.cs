using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    [Header("HEALTH")]
    [SerializeField] int _maxHealth;
    [SerializeField] int _noOfHearts;

    [Header("SPRITES")]
    [SerializeField] Image[] hearts;
    [SerializeField] Sprite _fullHealth;
    [SerializeField] Sprite _emptyHealth;

    public static int _currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        _healthChecker();
    }

    void _healthChecker()
    {
        if (_currentHealth > _noOfHearts)
        {
            _currentHealth = _noOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < _currentHealth)
            {
                hearts[i].sprite = _fullHealth;
            }
            else
            {
                hearts[i].sprite = _emptyHealth;

            }
            if (i < _noOfHearts)
            {
                hearts[i].enabled = true;

            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
