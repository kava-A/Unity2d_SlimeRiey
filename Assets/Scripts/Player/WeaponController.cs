using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    [SerializeField] private GameObject specialWeapon; // 特殊武器对象
    [SerializeField] private List<bool> weaponUnlockedStates = new List<bool>();
    private bool isActivateSpecialWeapon = false; // 是否激活特殊武器
    private int currentWeaponIndex = -1;
    private void Start()
    {
        if(specialWeapon!= null&&!isActivateSpecialWeapon)
        {
            specialWeapon.SetActive(false);//开局禁用特殊武器
        }
        if (weapons.Count == 0)
        {
            Debug.LogError("武器列表为空");
            return;
        }
        InitializeWeaponUnlocks();
        //开局禁用所有武器
        DisableOtherWeapon();
        ActivateFirstUnlockedWeapon();
    }
    private void InitializeWeaponUnlocks()
    {
        // 如果解锁状态列表数量与武器数量不匹配，自动调整
        while (weaponUnlockedStates.Count < weapons.Count)
        {
            weaponUnlockedStates.Add(false); // 新增武器默认未解锁
        }
        while (weaponUnlockedStates.Count > weapons.Count)
        {
            weaponUnlockedStates.RemoveAt(weaponUnlockedStates.Count - 1);
        }
    }
    // 激活第一把已解锁的武器
    private void ActivateFirstUnlockedWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (weaponUnlockedStates[i])
            {
                ActivateWeapon(i);
                return;
            }
        }
    }
    private void Update()
    {
        if (Time.timeScale == 0) return;
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput > 0.1f)
        {
            SwitchToNextWeapon();
        }
        else if (scrollInput < -0.1f)
        {
            SwitchToPreviousWeapon();
        }
    }
    private void DisableOtherWeapon()
    {
        foreach (GameObject weapon in weapons)
        {
            if(weapon != null) 
            weapon.SetActive(false);
        }
    }
    /// <summary>
    /// 外部调用激活武器
    /// </summary>
    /// <param name="index">武器对象索引</param>
    public void UnlockAndActivateWeapon(int index)
    {
        if(index < 0 || index >= weapons.Count)
        {
            Debug.LogError($"武器索引超出范围,索引最大为{weapons.Count-1}");
            return;
        }
        weaponUnlockedStates[index] = true; // 解锁武器
        ActivateWeapon(index);//激活武器
    }
    /// <summary>
    /// 激活下一个武器
    /// </summary>
    public void SwitchToNextWeapon()
    {
        if (weapons.Count == 0) return;

        int nextIndex = currentWeaponIndex;
        for (int i = 0; i < weapons.Count; i++)
        {
            nextIndex++;
            if (nextIndex >= weapons.Count)
                nextIndex = 0;

            // 找到第一个已解锁的武器
            if (weaponUnlockedStates[nextIndex])
            {
                ActivateWeapon(nextIndex);
                return;
            }
        }
    }

    /// <summary>
    /// 激活上一把武器
    /// </summary>
    public void SwitchToPreviousWeapon()
    {
        if (weapons.Count == 0) return;

        int prevIndex = currentWeaponIndex;
        for (int i = 0; i < weapons.Count; i++)
        {
            prevIndex--;
            if (prevIndex < 0)
                prevIndex = weapons.Count - 1;

            // 找到第一个已解锁的武器
            if (weaponUnlockedStates[prevIndex])
            {
                ActivateWeapon(prevIndex);
                return;
            }
        }
    }

    private void ActivateWeapon(int index)
    {
        GameObject targetWeapon = weapons[index];
        if (targetWeapon == null)
        {
            Debug.LogError($"索引为 {index} 的武器对象为空！请检查武器列表配置");
            return;
        }

        // 检查武器是否已解锁
        if (!weaponUnlockedStates[index])
        {
            Debug.LogWarning($"武器 {index} 未解锁，无法激活");
            return;
        }

        // 如果要激活的是当前已激活的武器，直接返回
        if (currentWeaponIndex == index && targetWeapon.activeSelf)
        {
            return;
        }

        // 禁用当前激活的武器
        if (currentWeaponIndex != -1 && currentWeaponIndex < weapons.Count)
        {
            GameObject currentWeapon = weapons[currentWeaponIndex];
            if (currentWeapon != null)
            {
                currentWeapon.SetActive(false);
            }
        }
        targetWeapon.SetActive(true);
        currentWeaponIndex = index;
       
    }
    public void SpcialWeapon()
    {
        specialWeapon.SetActive(true);
        isActivateSpecialWeapon = true;
    }

    /// <summary>
    /// 检查武器是否已解锁
    /// </summary>
    public bool IsWeaponUnlocked(int index)
    {
        if (index < 0 || index >= weaponUnlockedStates.Count)
            return false;

        return weaponUnlockedStates[index];
    }
}
