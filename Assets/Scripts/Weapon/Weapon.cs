using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    public abstract void Shoot();

    public class Pistol : Weapon
    {
        public override void Shoot()
        {
            // ʵ������߼�
        }
    }

    public class Shotgun : Weapon
    {
        public override void Shoot()
        {
            // ʵ������߼�
        }
    }
}
