using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Blaster : MonoBehaviour
{

    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] Transform _muzzle;
    [SerializeField] AudioClip _fireSound;

    public float CapacitorChargePercentage => _capacitor / _maxCapacitor;
    public float CoolDownPercent => Mathf.Clamp(_coolDown / _coolDownTime, 0f, 1f);
    float _capacitor, _maxCapacitor, _costPerShot, _rechargeRate;
    int _damage;
     Rigidbody _rigidbody;
    float _coolDown, _launchForce, _duration;
    float _coolDownTime;
    iWeaponControls _weaponInput;

    AudioSource _audioSource;

    bool CanFire
    {
        get
        {
            _coolDown -= Time.deltaTime;
            return _coolDown <= 0f;
        }
    }
     void Awake()
    {
        _audioSource = SoundManager.Configure3DAudioSource(GetComponent<AudioSource>());
    }
    // Update is called once per frame
    void Update()
    {
        _capacitor = Mathf.Min(_maxCapacitor, _capacitor + (_rechargeRate * Time.deltaTime));
        if (_weaponInput == null) return;
        if(CanFire && _weaponInput.PrimaryWeapon)
        {
            FireProjectile();
        }
    }
    internal void Init(iWeaponControls weaponInput, float coolDown, float blasterLauncheForce, float blasterDuration, int blasterDamage, Rigidbody rigidbody, float maxCapacitor = 1000f, float costPerShot = 20f, float rechargeRate = 40f)
    {
        _weaponInput = weaponInput;
        _coolDownTime = coolDown;
        _launchForce = blasterLauncheForce;
        _duration = blasterDuration;
        _damage = blasterDamage;
        _rigidbody = rigidbody;
        _capacitor = _maxCapacitor = maxCapacitor;
        _costPerShot = costPerShot;
        _rechargeRate = rechargeRate;
    }

    void FireProjectile()
    {
        if (_fireSound)
        {
            _audioSource.PlayOneShot(_fireSound, 0.6f);
        }
        _coolDown = _coolDownTime;
        bool fullCharge = _capacitor >= _costPerShot;
        _capacitor = Mathf.Max(_capacitor - _costPerShot, 0);
        Projectile projectile = Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
        projectile.gameObject.SetActive(false);
        projectile.Init(_launchForce, fullCharge ? _damage : (int)(_damage * 0.4f), _duration, _rigidbody.velocity, _rigidbody.angularVelocity);
        projectile.gameObject.SetActive(true);
    }

}
