using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipEngine : MonoBehaviour
{
    [SerializeField] GameObject _thruster;
    iMovementControls _shipMovementControls;
    bool isThrusting => !Mathf.Approximately(0f, _shipMovementControls.thrustAmount);

    Rigidbody _rigidbody;
    float _thrustForce;
    float _thrustAmount = 0f;
    // Update is called once per frame
    void Update()
    {
        ActiveThruster();
    }
    private void FixedUpdate()
    {
        if (!isThrusting) return;
        _rigidbody.AddForce(transform.forward * (_thrustAmount * Time.fixedDeltaTime));
    }
    void ActiveThruster()
    {
        _thruster.SetActive(isThrusting);
        if (!isThrusting) return;
        _thrustAmount = _thrustForce * _shipMovementControls.thrustAmount;
    }

    public void Init(iMovementControls movementControls, Rigidbody rigidbody, float thrustForce)
    {
        _rigidbody = rigidbody;
        _shipMovementControls = movementControls;
        _thrustForce = thrustForce;
    }
}
