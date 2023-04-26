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
            // 实现射击逻辑
        }
    }

    public class Shotgun : Weapon
    {
        public override void Shoot()
        {
            // 实现射击逻辑
        }
    }
}
