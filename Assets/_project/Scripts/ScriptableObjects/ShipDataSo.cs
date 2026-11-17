using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ShipData", menuName = "SolarDefenderBeta/Ship Data", order = 1)]
public class ShipDataSo : ScriptableObject
{
    //Ship controller

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

    [Header("Ship components values")]
    [SerializeField]
    public int
    //shield/health
        _shieldStrength = 3000;
    [SerializeField]
    public int
        _shieldRegen = 500,
        _maxHealth = 5000;
    //blaster
    [Range(10, 1000)][SerializeField] public int _blasterDamage = 100;
    [Range(5000f, 50000f)][SerializeField] public float _blasterLauchForce = 12000f;
    [Range(1f, 10f)][SerializeField] float _blasterProjectileDuration = 2f;
    [Range(0f, 0.5f)][SerializeField] float _blasterCoolDown = 0.18f;


    //movement
    public float ThrustForce => _thrustForce;
    public float PitchForce => _pitchForce;
    public float RollForce => _rollForce;
    public float YawForce => _yawForce;
    //components values
    public int ShieldStrength => _shieldStrength;
    public int ShieldRegen => _shieldRegen;
    public int MaxHealth => _maxHealth;
    public int BlasterDamage => _blasterDamage;
    public float BlasterProjectileDuration => _blasterProjectileDuration;
    public float BlasterCoolDown => _blasterCoolDown;
    public float BlasterLaunchForce => _blasterLauchForce;
}
