using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Gun Data")]
public class gunData : ScriptableObject
{
    public string _name;
    public GameObject _bulletPrefab;
    public float _bulletForce;
    public float _startTimeBtwShots;

}
