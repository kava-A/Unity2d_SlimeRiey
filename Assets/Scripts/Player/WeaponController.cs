using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();

    private int currentWeaponIndex = -1;
    private void Start()
    {
        //开局禁用所有武器
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
    /// 外部调用激活武器
    /// </summary>
    /// <param name="index">武器对象索引</param>
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
        currentWeaponIndex = Mathf.Clamp(currentWeaponIndex, 0, weapons.Count - 1);//限制武器索引范围在0到武器数量-1之间
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
