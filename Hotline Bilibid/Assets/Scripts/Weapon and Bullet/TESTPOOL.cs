using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTPOOL : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    public static TESTPOOL Instance { get; private set; }
    private Queue<GameObject> objects = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        AddObjects(100);
    }

    public GameObject Get()
    {
        if(objects.Count == 0)
        {
            AddObjects(1);
        }
        return objects.Dequeue();
    }

    public void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.SetActive(false);
        objects.Enqueue(objectToReturn);
    }

    void AddObjects(int count)
    {
        var newObject = GameObject.Instantiate(prefab);
        newObject.SetActive(false);
        objects.Enqueue(newObject);
    }
}
