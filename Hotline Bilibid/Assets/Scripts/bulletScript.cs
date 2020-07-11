using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [SerializeField] float _lifetimeInSeconds;
    private void Start()
    {
        StartCoroutine(_bulletDecay());
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                Destroy(other.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    IEnumerator _bulletDecay()
    {
        yield return new WaitForSeconds(_lifetimeInSeconds);
        Destroy(this.gameObject);
    }

}
