using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    [SerializeField] Projectile _projectilePrefab;

    [SerializeField] Transform _muzzle;
    int _damage;
    float _coolDown, _launchForce, _duration;
    float _coolDownTime;
    iWeaponControls _weaponInput;

    bool CanFire
    {
        get
        {
            _coolDown -= Time.deltaTime;
            return _coolDown <= 0f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (_weaponInput == null) return;
        if(CanFire && _weaponInput.PrimaryWeapon)
        {
            FireProjectile();
        }
    }
    internal void Init(iWeaponControls weaponInput, float coolDown, float blasterLauncheForce, float blasterDuration, int blasterDamage)
    {
        _weaponInput = weaponInput;
        _coolDownTime = coolDown;
        _launchForce = blasterLauncheForce;
        _duration = blasterDuration;
        _damage = blasterDamage;
    }

    void FireProjectile()
    {
        _coolDown = _coolDownTime;
        Projectile projectile = Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
        projectile.gameObject.SetActive(false);
        projectile.Init(_launchForce, _damage, _duration);
        projectile.gameObject.SetActive(true);

    }

}
