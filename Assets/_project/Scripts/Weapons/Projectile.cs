using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Range(5000f, 50000f)] [SerializeField] float _launchForce = 25000f;

    [Range(10, 1000)] [SerializeField] int _damage = 100;

    [Range(2f, 10f)] [SerializeField] float _range = 5f;

    bool OutOfFuel
    {
        get
        {
            _duration -= Time.deltaTime;
            return _duration <= 0;
        }
    }

    Rigidbody _rigidBody;
    float _duration;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rigidBody.AddForce(_launchForce *transform.forward);
        _duration = _range;
    }
    private void Update()
    {
        if (OutOfFuel) Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"projectile collided with {collision.collider.name}");
    }
}
