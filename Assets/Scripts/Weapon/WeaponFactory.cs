using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class WeaponFactory
{
    public enum WeaponType
    {
        Pistol,
        Shotgun
    }

    public static Weapon CreateWeapon(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Pistol:
                return new Pistol();
            case WeaponType.Shotgun:
                return new Shotgun();
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}
