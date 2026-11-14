using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] Projectile _projectilePrefab;

    [SerializeField] Transform _muzzle;

    [Range(0f, 5f)] [SerializeField] float _coolDownTime = 0.2f;

    bool CanFire
    {
        get
        {
            _coolDown -= Time.deltaTime;
            return _coolDown <= 0f;
        }
    }
    float _coolDown;
    // Update is called once per frame
    void Update()
    {
        if(CanFire && Input.GetMouseButton(0))
        {
            FireProjectile();
        }  
    }
    void FireProjectile()
    {
        _coolDown = _coolDownTime;
        Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
    }
}
