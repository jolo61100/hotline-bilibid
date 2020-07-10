using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameplayScript : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI _timer;
    [SerializeField] float _timeToNextWave = 10.0f;
    float _currentTime;
    [Header("SPAWN")]
    [SerializeField] GameObject[] _objectsEnemy;


    // Start is called before the first frame update
    void Start()
    {
        _currentTime = _timeToNextWave;
        StartCoroutine(game());
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime -= Time.deltaTime;
        _timer.text = "NEXT WAVE: " + (int)_currentTime;
    }

    public IEnumerator game()
    {
        yield return new WaitForSeconds(_timeToNextWave);
        _currentTime = _timeToNextWave;
        SpawnEnemy();
        StartCoroutine(game());
    }

    void SpawnEnemy()
    {
        int _rand = Random.Range(0, _objectsEnemy.Length);
        Instantiate(_objectsEnemy[_rand], transform.position, Quaternion.identity);
    }
}
