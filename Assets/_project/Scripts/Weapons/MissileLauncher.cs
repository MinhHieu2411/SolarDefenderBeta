using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class MissileLauncher : MonoBehaviour
{
    [SerializeField] GameObject _misslePrefab;
    [SerializeField] int _missiles = 4, _reload = 4;
    [SerializeField] float _coolDown = 2f, _reloadTime = 10f;
    [SerializeField] AudioClip _launchSound;


    public int MissileCapacity => _missiles;
    public int Missiles => _missilreRemaining;
    public float ReloadPercent => 1f - (_reloadDelay/_reloadTime);
    public int Reloads => _reloadRemaining;
    public bool Reloading => _missilreRemaining < 1 && _reloadRemaining > 0 && _reloadDelay > 0f;
    public UnityEvent MissileFired, MissileReloaded;
    AudioSource _audioSource;

    Transform _transform;
    RadarScreen _radarScreen;
    int _missilreRemaining, _reloadRemaining;
    float _fireDelay, _reloadDelay;
    bool CanFire
    {
        get
        {
            if (_missilreRemaining < 1) return false;
            _fireDelay -= Time.deltaTime;
            return _fireDelay <= 0f;
        }
    }
    bool CanReload
    {
        get
        {
            if (_missilreRemaining > 0 ||  _reloadRemaining < 1) return false;
            _reloadDelay -= Time.deltaTime;
            return _reloadDelay <= 0f;
        }
    }
     void Awake()
    {
        MissileFired = new UnityEvent();
        MissileReloaded = new UnityEvent();
        _transform = transform;
        _radarScreen = FindObjectOfType<RadarScreen>();
        _audioSource = SoundManager.Configure3DAudioSource(GetComponent<AudioSource>());
    }
     void OnEnable()
    {
        _missilreRemaining = _missiles;
        _reloadRemaining = _reload;
        _fireDelay = 0f;
        _reloadDelay = _reloadTime;
    }
     void Update()
    {
        if (CanReload)
        {
            ReloadMissile();
        }
        if(CanFire && Input.GetMouseButtonDown(1))
        {
            FireMissile();
        }
    }

     void FireMissile()
    {
        if (_launchSound)
        {
            _audioSource.PlayOneShot(_launchSound, 2f);
        }
        var missile = Instantiate(_misslePrefab, _transform.position, _transform.rotation).GetComponent<Missile>();
        if (_radarScreen)
        {
            missile.Init(_radarScreen.LockOnTarget);
        }
        missile.gameObject.SetActive(true);
        _missilreRemaining--;
        _fireDelay = _coolDown;

        MissileFired.Invoke();
    }

     void ReloadMissile()
    {
        _reloadRemaining--;
        _missilreRemaining = _missiles;
        _reloadDelay = _reloadTime;
        _fireDelay = 0f;

        MissileReloaded.Invoke();
    }
}
