using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, iDamageable
{
    [SerializeField] private FractureAsteroid _fractureAsteroidPrefab;
    [SerializeField] private Detonator _explosionPrefab;
    Transform _transform;
    private void Awake()
    {
        _transform = transform;
    }
    public void TakeDamage(int Damage, Vector3 hitPosition)
    {
        FractureAsteroid(hitPosition);
    }

    private void FractureAsteroid(Vector3 hitPosition)
    {
        if(_fractureAsteroidPrefab != null)
        {
            Instantiate(_fractureAsteroidPrefab, _transform.position, _transform.rotation);
        }
    }
}
