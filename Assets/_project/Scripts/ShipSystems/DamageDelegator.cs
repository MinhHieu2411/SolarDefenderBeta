using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDelegator : MonoBehaviour, iDamageable
{
    [SerializeField] GameObject[] _damageDelegates;
    List<iDamageable> _damageReceivers;

    private void Awake()
    {
        if (_damageDelegates == null) return;
        _damageReceivers = new List<iDamageable>();
        foreach(GameObject damageDelegate in _damageDelegates)
        {
            if (damageDelegate.TryGetComponent<iDamageable>(out var damageable))
            {
                _damageReceivers.Add(damageable);
            }
        }
    }
    public void TakeDamage(int Damage, Vector3 hitPosition)
    {
        if(_damageReceivers == null) return;
        foreach(iDamageable damageable in _damageReceivers)
        {
            damageable.TakeDamage(Damage, hitPosition);
        }
    }
}
