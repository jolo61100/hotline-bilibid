using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthitemPool : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Vector2[] spawnPoint;

    public static HealthitemPool Instance { get; private set; }
    private Queue<GameObject> objects = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
    }


    public GameObject Get()
    {
        if (objects.Count == 0)
        {
            AddObjects();
        }
        return objects.Dequeue();
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        int rand = Random.Range(0, spawnPoint.Length);
        objectToReturn.transform.position = spawnPoint[rand]; // replace this with the call the first time it has spawn
        objectToReturn.SetActive(false);
        objects.Enqueue(objectToReturn);


    }

    void AddObjects()
    {
        
        int rand = Random.Range(0, spawnPoint.Length);
        transform.position = spawnPoint[rand];
        var health = GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
        health.SetActive(false);
        objects.Enqueue(health);
        
    }
}
