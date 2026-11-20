using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShipController : MonoBehaviour
{
    [SerializeField] Shield _shield;
    [Header("Ship input controls")] [SerializeField] MovementControlsBase _movementControls;
    [SerializeField] WeaponControlsBase _weaponControls;
    [Header("Movement values")] [SerializeField] ShipDataSo _shipData;

    [Header("Ship components")]
    [SerializeField] List<ShipEngine> _engines;

    [Header("Ship components")]
    [SerializeField] List<Blaster> _blasters;


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

    DamageHandler _damageHandler;

    iMovementControls MovementInput => _movementControls;
    iWeaponControls WeaponInput => _weaponControls;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        _damageHandler = GetComponent<DamageHandler>();
    }

    void Start()
    {
        foreach (ShipEngine engine  in _engines)
        {
            engine.Init(MovementInput, _rigidbody, _shipData.ThrustForce/_engines.Count);
        }
        foreach(Blaster blaster in _blasters)
        {
            blaster.Init(WeaponInput, _shipData.BlasterCoolDown,_shipData.BlasterLaunchForce, _shipData.BlasterProjectileDuration, _shipData.BlasterDamage);
        }
        //_cockpitControls.Init(MovementInput);
    }
    private void OnEnable()
    {
        if (_damageHandler == null) return;
        _damageHandler.Init(_shipData.MaxHealth);
        _damageHandler.HealthChanged.AddListener(OnHealthChanged);
        _damageHandler.ObjectDestroyEvent.AddListener(DestroyShip);
    }
    void Update()
    {
        //_thrustAmount = ControlInput.thrustAmount;
        _pitchAmount = MovementInput.pitchAmount;
        _rollAmount = MovementInput.rollAmount;
        _yawAmount = MovementInput.yawAmount;
    }

    //ship movements and env around
    void FixedUpdate()
    {
        //player movement
        if(!Mathf.Approximately(0f, _pitchAmount))
        {
            _rigidbody.AddTorque(transform.right * (_shipData.PitchForce*_pitchAmount*Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, _rollAmount))
        {
            _rigidbody.AddTorque(transform.forward * (_shipData.RollForce * _rollAmount *Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, _yawAmount))
        {
            _rigidbody.AddTorque(transform.up *(_yawAmount *_shipData.YawForce * Time.fixedDeltaTime));
        }
        /*if(!Mathf.Approximately(0f, _thrustAmount))
        {
            _rigidbody.AddForce(transform.forward * (_thrustForce * _thrustAmount * Time.fixedDeltaTime));
        }*/
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{gameObject.name} collided with {collision.collider.name}");
    }*/


    private void OnHealthChanged()
    {
        Debug.Log($"{gameObject.name} health is {_damageHandler.Health}/{_damageHandler.MaxHealth}");
    }
    private void DestroyShip()
    {
        gameObject.SetActive(false);
    }

}
