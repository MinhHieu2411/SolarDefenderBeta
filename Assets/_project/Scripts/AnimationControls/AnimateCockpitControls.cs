using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCockpitControls : MonoBehaviour
{
    [Header("Control transform and ranges")] [SerializeField] Transform _joystick;

    [Header("Control transform and ranges")][SerializeField] Vector3 _joystickRange = Vector3.zero;

    [Header("Control transform and ranges")][SerializeField] List<Transform> _throttles;

    [Header("Control transform and ranges")][SerializeField] float _throttleRange = 5f;

    //[SerializeField] ShipMovementInput _movementInput;

    iMovementControls _movementInput;
    void Update()
    {
        if (_movementInput == null) return;
        _joystick.localRotation = Quaternion.Euler(
            _movementInput.pitchAmount * _joystickRange.x,
            _movementInput.yawAmount * _joystickRange.y,
            _movementInput.rollAmount * _joystickRange.z
            );

        Vector3 throttleRotation = _throttles[0].localRotation.eulerAngles;
        throttleRotation.x = _movementInput.thrustAmount * _throttleRange;
        foreach(Transform throttle in _throttles)
        {
            throttle.localRotation = Quaternion.Euler(throttleRotation);
        }

    }
    public void Init(iMovementControls movementControls)

    {
        if (_movementInput == null) return;
        _movementInput = movementControls;
    }
}
