using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] ShipMovementInput _movementInput;

    //adjust the physic values in unity
    [Header("Movement values")]
    //engine thrust
    [SerializeField]
    [Range(1000f, 12000f)]
    float _thrustForce = 5000f;
    //up-down
    [SerializeField]
    [Range(1000f, 10000f)]
    float _pitchForce = 2000f;
    //rolling
    [SerializeField]
    [Range(1000f, 10000f)]
    float _rollForce = 3000f;
    //turn left-right
    [SerializeField]
    [Range(1000f, 10000f)]
    float _yawForce = 2000f;


    [Header("Ship components")]
    [SerializeField] List<ShipEngine> _engines;

    [Header("Ship components")][SerializeField] AnimateCockpitControls _cockpitControls;

    Rigidbody _rigidbody;

    [Header("Simulation values")]
    /*[Range(0f, 2f)]
    [SerializeField] float _thrustAmount = 0f;*/

    [Range(-1f, 1f)]
    [SerializeField] float _pitchAmount = 0f;

    [Range(-1f, 1f)]
    [SerializeField] float _rollAmount = 0f;

    [Range(-1f, 1f)]
    [SerializeField] float _yawAmount = 0f;

    iMovementControls ControlInput => _movementInput.MovementControls;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Start()
    {
        foreach (ShipEngine engine  in _engines)
        {
            engine.Init(ControlInput, _rigidbody, _thrustForce/_engines.Count);
        }
        _cockpitControls.Init(ControlInput);
    }

    void Update()
    {
        //_thrustAmount = ControlInput.thrustAmount;
        _pitchAmount = ControlInput.pitchAmount;
        _rollAmount = ControlInput.rollAmount;
        _yawAmount = ControlInput.yawAmount;
    }

    //ship movements and env around
    void FixedUpdate()
    {
        //player movement
        if(!Mathf.Approximately(0f, _pitchAmount))
        {
            _rigidbody.AddTorque(transform.right * (_pitchForce*_pitchAmount*Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, _rollAmount))
        {
            _rigidbody.AddTorque(transform.forward * (_rollForce * _rollAmount *Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, _yawAmount))
        {
            _rigidbody.AddTorque(transform.up *(_yawAmount * _yawForce * Time.fixedDeltaTime));
        }
        /*if(!Mathf.Approximately(0f, _thrustAmount))
        {
            _rigidbody.AddForce(transform.forward * (_thrustForce * _thrustAmount * Time.fixedDeltaTime));
        }*/
    }
}
