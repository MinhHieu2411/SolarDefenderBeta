using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField] GameObject _misslePrefab;
    [SerializeField] int _missile = 4, _reload = 2;
    [SerializeField] float _coolDown = 2f, _reloadTime = 60f;

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
        _transform = transform;
        _radarScreen = FindObjectOfType<RadarScreen>();
    }
     void OnEnable()
    {
        _missilreRemaining = _missile;
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
        var missile = Instantiate(_misslePrefab, _transform.position, _transform.rotation).GetComponent<Missile>();
        if (_radarScreen)
        {
            missile.Init(_radarScreen.LockOnTarget);
        }
        missile.gameObject.SetActive(true);
        _missilreRemaining--;
        _fireDelay = _coolDown;
    }

     void ReloadMissile()
    {
        _reloadRemaining--;
        _missilreRemaining = _missile;
        _reloadDelay = _reloadTime;
    }
}
