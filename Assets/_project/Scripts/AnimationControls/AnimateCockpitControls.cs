using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCockpitControls : MonoBehaviour
{
    [Header("Control transform and ranges")] [SerializeField] Transform _joystick;

    [Header("Control transform and ranges")][SerializeField] Vector3 _joystickRange = Vector3.zero;

    [Header("Control transform and ranges")][SerializeField] List<Transform> _throttles;

    [Header("Control transform and ranges")][SerializeField] float _throttleRange = 15f;

    [SerializeField] ShipMovementInput _movementInput;

    iMovementControls ControlInput => _movementInput.MovementControls;
    void Update()
    {
        _joystick.localRotation = Quaternion.Euler(
            ControlInput.pitchAmount * _joystickRange.x,
            ControlInput.yawAmount * _joystickRange.y,
            ControlInput.rollAmount * _joystickRange.z
            );

        /*Vector3 throttleRotation = _throttles[0].localRotation.eulerAngles;
        throttleRotation.x = ControlInput.thrustAmount * _throttleRange;
        foreach(Transform throttle in _throttles)
        {
            throttle.localRotation = Quaternion.Euler(throttleRotation);
        }*/

    }
}
