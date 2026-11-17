using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float _launchForce;
    int _damage;
    float _range;
    float _duration;
    Rigidbody _rigidBody;
    [SerializeField] Detonator _hitEffect;

    bool OutOfFuel
    {
        get
        {
            _duration -= Time.deltaTime;
            return _duration <= 0;
        }
    }

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rigidBody.AddForce(_launchForce *transform.forward);
        _duration = _range;
    }
    private void OnDisable()
    {
        _rigidBody.velocity = Vector3.zero;
        _rigidBody.angularVelocity = Vector3.zero;
    }
    private void Update()
    {
        if (OutOfFuel) Destroy(gameObject);
    }
    public void Init(float launchForce,int damage, float range)
    {
        _launchForce = launchForce;
        _damage = damage;
        _range = range;
    }
    private void OnCollisionEnter(Collision collision)
    {
        iDamageable damageable = collision.collider.gameObject.GetComponent<iDamageable>();
        if(damageable != null)
        {
            Vector3 hitPosition = collision.GetContact(0).point;
            damageable.TakeDamage(_damage, hitPosition);
        }
        if (_hitEffect != null)
        {
            Instantiate(_hitEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
