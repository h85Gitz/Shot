using UnityEngine;

public class Player : MonoBehaviour
{
    private Weapon _currentWeapon;

    private void Start()
    {
        // ≥ı ºŒ‰∆˜Œ™ ÷«π
        _currentWeapon = WeaponFactory.CreateWeapon(WeaponFactory.WeaponType.Pistol);
    }

    private void Update()
    {
        // ºÏ≤‚ÕÊº“ ‰»Î£¨«–ªªŒ‰∆˜
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentWeapon = WeaponFactory.CreateWeapon(WeaponFactory.WeaponType.Pistol);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentWeapon = WeaponFactory.CreateWeapon(WeaponFactory.WeaponType.Shotgun);
        }

        // ºÏ≤‚…‰ª˜ ‰»Î
        if (Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot();
        }
    }
}
