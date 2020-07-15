using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Vector2[] spawnPoint;

    public static EnemyPool Instance { get; private set; }
    public Queue<GameObject> objects = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        //AddObjects(10);
    }


    public GameObject Get()
    {
        if (objects.Count == 0 )
        {
            AddObjects(1);
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

    void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, spawnPoint.Length);
            transform.position = spawnPoint[rand];
            var test = GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
            test.SetActive(false);
            objects.Enqueue(test);
           }
    }
}
