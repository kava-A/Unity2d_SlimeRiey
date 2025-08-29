using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    [SerializeField] private GameObject specialWeapon; // ������������
    [SerializeField] private List<bool> weaponUnlockedStates = new List<bool>();
    private bool isActivateSpecialWeapon = false; // �Ƿ񼤻���������
    private int currentWeaponIndex = -1;
    private void Start()
    {
        if(specialWeapon!= null&&!isActivateSpecialWeapon)
        {
            specialWeapon.SetActive(false);//���ֽ�����������
        }
        if (weapons.Count == 0)
        {
            Debug.LogError("�����б�Ϊ��");
            return;
        }
        InitializeWeaponUnlocks();
        //���ֽ�����������
        DisableOtherWeapon();
        ActivateFirstUnlockedWeapon();
    }
    private void InitializeWeaponUnlocks()
    {
        // �������״̬�б�����������������ƥ�䣬�Զ�����
        while (weaponUnlockedStates.Count < weapons.Count)
        {
            weaponUnlockedStates.Add(false); // ��������Ĭ��δ����
        }
        while (weaponUnlockedStates.Count > weapons.Count)
        {
            weaponUnlockedStates.RemoveAt(weaponUnlockedStates.Count - 1);
        }
    }
    // �����һ���ѽ���������
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
    /// �ⲿ���ü�������
    /// </summary>
    /// <param name="index">������������</param>
    public void UnlockAndActivateWeapon(int index)
    {
        if(index < 0 || index >= weapons.Count)
        {
            Debug.LogError($"��������������Χ,�������Ϊ{weapons.Count-1}");
            return;
        }
        weaponUnlockedStates[index] = true; // ��������
        ActivateWeapon(index);//��������
    }
    /// <summary>
    /// ������һ������
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

            // �ҵ���һ���ѽ���������
            if (weaponUnlockedStates[nextIndex])
            {
                ActivateWeapon(nextIndex);
                return;
            }
        }
    }

    /// <summary>
    /// ������һ������
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

            // �ҵ���һ���ѽ���������
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
            Debug.LogError($"����Ϊ {index} ����������Ϊ�գ����������б�����");
            return;
        }

        // ��������Ƿ��ѽ���
        if (!weaponUnlockedStates[index])
        {
            Debug.LogWarning($"���� {index} δ�������޷�����");
            return;
        }

        // ���Ҫ������ǵ�ǰ�Ѽ����������ֱ�ӷ���
        if (currentWeaponIndex == index && targetWeapon.activeSelf)
        {
            return;
        }

        // ���õ�ǰ���������
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
    /// ��������Ƿ��ѽ���
    /// </summary>
    public bool IsWeaponUnlocked(int index)
    {
        if (index < 0 || index >= weaponUnlockedStates.Count)
            return false;

        return weaponUnlockedStates[index];
    }
}
