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
    [SerializeField] GameObject[] _EnemySpawner;

    public static bool _isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        _gameOver.text = string.Empty;
        _currentTime = _timeToNextWave;
        StartCoroutine(game());
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
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
        StartCoroutine(game());
    }

    IEnumerator gameEnd()
    {
        _gameOver.text = "GAME OVER";
        yield return new WaitForSeconds(1.0f);
        _playerScore = 0;
        _isAlive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void SpawnEnemy()
    {
        int rand = Random.Range(0, _EnemySpawner.Length);
        int rand1 = Random.Range(0, _EnemySpawner.Length);
        Instantiate(_EnemySpawner[rand], transform.position, Quaternion.identity);
        Instantiate(_EnemySpawner[rand1], transform.position, Quaternion.identity);
        Debug.Log(rand + "," + rand1);
    }
}
