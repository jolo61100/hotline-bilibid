using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nullScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(_destroyObject());
    }

    IEnumerator _destroyObject()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
