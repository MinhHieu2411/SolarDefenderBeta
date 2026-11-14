using System;
using UnityEngine;

[Serializable]
public class DesktopMovementControls : MovementControlsBase
{
    //limit the cursor zone when transform to movements
    [SerializeField] float _deadZoneRadius = 0.1f;
    Vector2 screenCenter => new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

    public override float yawAmount
    {
        get
        {
            Vector3 mousePosition = Input.mousePosition;
            float yaw = (mousePosition.x - screenCenter.x) / screenCenter.x;
            return Mathf.Abs(yaw) > _deadZoneRadius ? yaw : 0f;
        }
    }
    public override float pitchAmount
    {
        get
        {
            Vector3 mousePosition = Input.mousePosition;
            float pitch = (-mousePosition.y + screenCenter.y) / screenCenter.y;
            return Mathf.Abs(pitch) > _deadZoneRadius ? pitch : 0f;
        }
    }
    public override float rollAmount
    {
        get
        {
            float roll;
            if (Input.GetKey(KeyCode.A))
            {
                roll = 1f;

            }
            else
            {
                roll = Input.GetKey(KeyCode.D) ? -1f : 0f;
            }
            _rollAmount = Mathf.Lerp(_rollAmount, roll, Time.deltaTime * 3f);
            return _rollAmount;
        }
    }
    //basic thruster, heading to use s and w as stepped thruster
    //public override float thrustAmount => Input.GetAxis("Vertical");
    //stepped thruster
    float _thrustAmount = 0f;
    [SerializeField] float _thrustStep = 0.5f;
    private float _rollAmount;

    public override float thrustAmount
    {
        get
        {
            return _thrustAmount;
        }
    }
    public void UpdateThrust()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _thrustAmount += _thrustStep;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _thrustAmount -= _thrustStep;
        }
        _thrustAmount = Mathf.Clamp(_thrustAmount, 0f, 2f);
    }

}
