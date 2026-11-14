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

    iMovementControls _movementControls;
    void Update()
    {
        if (_movementControls == null) return;
        _joystick.localRotation = Quaternion.Euler(
            _movementControls.pitchAmount * _joystickRange.x,
            _movementControls.yawAmount * _joystickRange.y,
            _movementControls.rollAmount * _joystickRange.z
            );

        Vector3 throttleRotation = _throttles[0].localRotation.eulerAngles;
        throttleRotation.x = _movementControls.thrustAmount * _throttleRange;
        foreach(Transform throttle in _throttles)
        {
            throttle.localRotation = Quaternion.Euler(throttleRotation);
        }

    }
    public void Init(iMovementControls movementControls)
    {
        _movementControls = movementControls;
    }
}
