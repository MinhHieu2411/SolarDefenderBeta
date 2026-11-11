using System;
using UnityEngine;

public class ShipInputManager : MonoBehaviour
{
    public enum InputType
    {
        PlayerDesktop,
        PlayerMobile,
        Bot
    }

    public static iMovementControls GetInputcontrols(InputType inputType)
    {
        return inputType switch
        {
            InputType.PlayerDesktop => new DesktopMovementControls(),
            //develop later
            InputType.PlayerMobile => null,
            InputType.Bot => null,
            _ => throw new ArgumentOutOfRangeException(nameof(inputType), inputType, null)
        };
    }
}
