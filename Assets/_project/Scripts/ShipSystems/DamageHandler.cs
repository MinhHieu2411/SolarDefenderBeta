using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageHandler : MonoBehaviour, iDamageable
{
    [SerializeField] GameObject _explosionPrefab;
    UnityEvent _healthChangeEvent;
    UnityEvent _objectDestroyEvent;
    public int MaxHealth { get; private set; }
    public int Health { get; private set; }
    public UnityEvent HealthChanged
    {
        get
        {
            if (_healthChangeEvent == null)
            {
                _healthChangeEvent = new UnityEvent();
            }
            return _healthChangeEvent;
        }
    }
    public UnityEvent ObjectDestroyEvent
    {
        get
        {
            if( _objectDestroyEvent == null)
            {
                _objectDestroyEvent = new UnityEvent();
            }
            return _objectDestroyEvent;
        }
    }

    public void Init(int maxHealth)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
        HealthChanged.Invoke();
    }
    public void TakeDamage(int Damage, Vector3 hitPosition)
    {
        Health -=Damage;
        HealthChanged.Invoke();
        if (Health > 0) return;
        if (_explosionPrefab)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        }
         ObjectDestroyEvent.Invoke();
    }
    
}
