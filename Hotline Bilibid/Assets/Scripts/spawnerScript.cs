using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScript : MonoBehaviour
{
    [SerializeField] GameObject[] _objects;
    //[SerializeField] gameplayScript controller;

    private void Start()
    {
        // /StartCoroutine(controller.game());
        Spawn();
    }

    void Spawn()
    {
        int _rand = Random.Range(0, _objects.Length);
        Instantiate(_objects[_rand], transform.position, Quaternion.identity);
    }
}
