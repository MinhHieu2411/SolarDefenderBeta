using System;
using UnityEngine;

[Serializable]
public class DesktopWeaponControls : WeaponControlsBase
{
    public override bool PrimaryWeapon => Input.GetMouseButton(0);

    public override bool SecondaryWeapon => Input.GetMouseButton(1);
}