
using System;
using UnityEngine;

[Serializable]
public class PIDController
{
    public enum DerivativeMeasurement
    {
        Velocity,
        ErrorRateOfChange
    }

    [Header("PID Parameters")]
    [SerializeField] private float _proportionalGain;
    [SerializeField] private float _integralGain;
    [SerializeField] private float _derivativeGain;

    [Header("Output Limits")]
    [SerializeField] private float _minOutput;
    [SerializeField] private float _maxOutput;

    [Header("Integral Settings")]
    [SerializeField] private float _integralSaturation;

    [Header("Derivative Settings")]
    [SerializeField]
    private DerivativeMeasurement _derivativeMeasurement;

    [Header("PID Control")]
    [SerializeField] private bool _enablePid = true;

    public float _lastValue;
    public float _lastError;
    public float _integrationStored;
    public float _velocity;

    private bool _derivativeInitialized;

    public void Reset()
    {
        _derivativeInitialized = false;
    }

    public float Update(float deltaTime, float currentValue, float targetValue)
    {
        if (!_enablePid) return targetValue;

        if (deltaTime <= 0)
            throw new ArgumentOutOfRangeException(nameof(deltaTime));

        float error = targetValue - currentValue;

        // Proportional term
        float P = _proportionalGain * error;

        // Integral term
        _integrationStored = Mathf.Clamp(
            _integrationStored + (error * deltaTime),
            -_integralSaturation,
            _integralSaturation
        );

        float I = _integralGain * _integrationStored;

        // Derivative calculations
        float errorRateOfChange = (error - _lastError) / deltaTime;
        _lastError = error;

        float valueRateOfChange = (currentValue - _lastValue) / deltaTime;
        _lastValue = currentValue;

        _velocity = valueRateOfChange;

        // Select derivative measurement
        float derivativeMeasure = 0f;

        if (_derivativeInitialized)
        {
            if (_derivativeMeasurement == DerivativeMeasurement.Velocity)
            {
                derivativeMeasure = -valueRateOfChange;
            }
            else
            {
                derivativeMeasure = errorRateOfChange;
            }
        }
        else
        {
            _derivativeInitialized = true;
        }

        // Derivative term
        float D = _derivativeGain * derivativeMeasure;

        // PID output
        float result = P + I + D;

        return Mathf.Clamp(result, _minOutput, _maxOutput);
    }
}
