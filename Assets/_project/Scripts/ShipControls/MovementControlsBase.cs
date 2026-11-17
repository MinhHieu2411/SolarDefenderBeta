using UnityEngine;

public abstract class MovementControlsBase : MonoBehaviour, iMovementControls
{
    public abstract float yawAmount { get; }
    public abstract float pitchAmount { get; }
    public abstract float rollAmount { get; }
    public abstract float thrustAmount { get; }

}
