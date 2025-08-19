using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();

    private int currentWeaponIndex = -1;
    private void Start()
    {
        //���ֽ�����������
        DisableOtherWeapon();
    }
    private void DisableOtherWeapon()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }
    /// <summary>
    /// �ⲿ���ü�������
    /// </summary>
    /// <param name="index">������������</param>
    public void GetWeaponIndex(int index)
    {
        ActivateWeapon(index);
    }
    private void Update()
    {
        //if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        //{
        //    currentWeaponIndex -= 1;
        //}
        //else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        //{
        //    currentWeaponIndex += 1;
        //}
        currentWeaponIndex = Mathf.Clamp(currentWeaponIndex, 0, weapons.Count - 1);//��������������Χ��0����������-1֮��
        ActivateWeapon(currentWeaponIndex);

    }

    private void ActivateWeapon(int index)
    {
        if (weapons[index] == null && weapons[index].activeSelf)
        {
            return;
        }
        weapons[index].SetActive(true);
    }
}
