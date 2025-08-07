using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();

    private int currentWeaponIndex = 0;
    private void Update()
    {
        Mathf.Clamp(currentWeaponIndex, 0, weapons.Count - 1);//��������������Χ��0����������-1֮��
        if (Input.GetAxis("Mouse ScrollWheel") > 0.1f)
        {
            currentWeaponIndex -= 1;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < -0.1f)
        {
            currentWeaponIndex += 1;
        }
        ActivateWeapon(currentWeaponIndex);
    }

    private void ActivateWeapon( int index)
    {
        weapons[index].SetActive(true);
    }
}
