using UnityEngine;

public abstract class WeaponControlsBase : MonoBehaviour, iWeaponControls
{
    public abstract bool PrimaryWeapon { get; }

    public abstract bool SecondaryWeapon { get; }
}