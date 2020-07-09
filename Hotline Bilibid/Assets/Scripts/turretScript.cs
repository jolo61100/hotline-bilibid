using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretScript : MonoBehaviour
{
    Transform _target;
    RaycastHit2D _hitInfo;
    [SerializeField] LayerMask _layerMask;
    // Start is called before the first frame update
    void Start()
    {
        _target = FindObjectOfType<playerScript>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
    }

    void Aim()
    {
        _hitInfo = Physics2D.Linecast
        (
            transform.position,
            _target.position,
            _layerMask
        );

        if (_hitInfo.collider != null)
        {
            if (_hitInfo.collider.gameObject.CompareTag("Player"))
            {
                Debug.DrawLine(transform.position, _target.position, Color.red);
            }
            else
            {
                Debug.DrawLine(transform.position, _target.position, Color.yellow);
            }
        }
        else Debug.DrawLine(transform.position, _target.position, Color.green);

    }

}
