using UnityEngine;

public class Player : MonoBehaviour
{
    private Weapon _currentWeapon;

    private void Start()
    {
        // ��ʼ����Ϊ��ǹ
        _currentWeapon = WeaponFactory.CreateWeapon(WeaponFactory.WeaponType.Pistol);
    }

    private void Update()
    {
        // ���������룬�л�����
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentWeapon = WeaponFactory.CreateWeapon(WeaponFactory.WeaponType.Pistol);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentWeapon = WeaponFactory.CreateWeapon(WeaponFactory.WeaponType.Shotgun);
        }

        // ����������
        if (Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot();
        }
    }
}
