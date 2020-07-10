using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretScript : MonoBehaviour
{
    Rigidbody2D _rb;
    Transform _target;
    RaycastHit2D _hitInfo;
    [SerializeField] float _startTimeBtwShots;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] LineRenderer _lineOfSight;
    [SerializeField] Gradient _redColor;
    float _lineWidthDefault = 0.05f;
    float _lineWidthActive = 0.0005f;
    float _timeBtwShots;

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _target = FindObjectOfType<playerScript>().transform;
        _timeBtwShots = _startTimeBtwShots;
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
    }

    private void FixedUpdate()
    {
        lookAtPlayer();
    }

    void lookAtPlayer()
    {
        Vector2 lookDirection = (Vector2)_target.position - _rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = angle;
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
            _lineOfSight.SetPosition(1, _hitInfo.point);
            if (_hitInfo.collider.gameObject.CompareTag("Player"))
            {
                _lineOfSight.colorGradient = _redColor;
                TurretShoot();
                Debug.DrawLine(transform.position, _target.position, Color.red);
            }
            else
            {
                _lineOfSight.colorGradient = _redColor;
                _timeBtwShots = _startTimeBtwShots;
                _lineOfSight.startWidth = _lineWidthDefault;
                _lineOfSight.endWidth = _lineWidthDefault;
                Debug.DrawLine(transform.position, _target.position, Color.yellow);
            }
        }
        else Debug.DrawLine(transform.position, _target.position, Color.green);

        _lineOfSight.SetPosition(0, transform.position);

    }

    void TurretShoot()
    {
        if (_timeBtwShots <= 0)
        {
            _timeBtwShots = _startTimeBtwShots;
            _lineOfSight.startWidth = _lineWidthDefault;
            _lineOfSight.endWidth = _lineWidthDefault;
        }
        else
        {
            _timeBtwShots -= Time.deltaTime;
            _lineOfSight.startWidth += _lineWidthActive;
            _lineOfSight.endWidth += _lineWidthActive;
        }
    }

}
