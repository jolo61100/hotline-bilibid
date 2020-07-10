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
    public static bool _spawnWave = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
