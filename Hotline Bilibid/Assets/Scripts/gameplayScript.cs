using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameplayScript : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI _timer;
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] TextMeshProUGUI _gameOver;
    [SerializeField] float _timeToNextWave = 10.0f;
    float _currentTime;
    public static int _playerScore;

    [Header("SPAWN")]
    [SerializeField] GameObject[] _healthspawns;
    [SerializeField] GameObject[] _EnemySpawner;
    [SerializeField] EnemyPool ePool;
    [SerializeField] int enemyCount;

    public static bool _isAlive = true;


    public AudioClip impact;
    AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        _gameOver.text = string.Empty;
        _currentTime = _timeToNextWave;
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(game());
    }

    // Update is called once per frame
    void Update()
    {
        if (healthCounter._currentHealth < 1)
        {
            //audioSource.PlayOneShot(impact, 0.7F); //Play PlayerDeath SFX
            _isAlive = false;
        }
        if (!_isAlive)
        {
            StartCoroutine(gameEnd());
        }
        if (_isAlive)
        {
            _gameOver.text = string.Empty;
        }
        _currentTime -= Time.deltaTime;
        _timer.text = "NEXT WAVE: " + (int)_currentTime;
        _score.text = "SCORE: " + _playerScore;
    }

    public IEnumerator game()
    {
        yield return new WaitForSeconds(_timeToNextWave);
        _isAlive = true;
        _currentTime = _timeToNextWave;
        SpawnEnemy();
        SpawnHealth();
        StartCoroutine(game());
    }

    IEnumerator gameEnd()
    {
        _gameOver.text = "GAME OVER";
        yield return new WaitForSeconds(1.0f);
        _playerScore = 0;
        _isAlive = true;
        SceneManager.LoadScene("MainMenu");
    }

    void SpawnEnemy()//spawn count per wave
    {
        for (int i = 0; i < enemyCount; i++)
        {
            var test = EnemyPool.Instance.Get();
            test.gameObject.SetActive(true);
        }
    }

    void SpawnHealth()
    {
        var health = HealthitemPool.Instance.Get();
        health.gameObject.SetActive(true);
    }
}