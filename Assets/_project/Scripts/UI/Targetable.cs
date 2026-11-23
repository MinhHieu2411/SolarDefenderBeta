
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    Transform _targetTransform;
    bool _isRegistered;

    private void Awake()
    {
        Rigidbody rb = GetComponentInParent<Rigidbody>();

        _targetTransform = rb != null? rb.transform : transform.root;
    }

    private void OnEnable()
    {
        RegisterTarget();
    }

    private void OnDisable()
    {
        RemoveTarget();
    }

    private void OnDestroy()
    {
        RemoveTarget();
    }

    void RegisterTarget()
    {
        if (_isRegistered || _targetTransform == null) return;
        if (UIManager.Instance == null)return;
        UIManager.Instance.AddTarget(_targetTransform);
        _isRegistered = true;
    }

    void RemoveTarget()
    {
        if (!_isRegistered) return;
        if (UIManager.Instance != null && _targetTransform != null)
        {
            UIManager.Instance.RemoveTarget(_targetTransform);
        }
        _isRegistered = false;
    }
}
