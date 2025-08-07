using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();

    private int currentWeaponIndex = 0;
    private void Start()
    {
        foreach(GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }
    public void GetWeaponIndex(int index)
    {
        weapons[index].SetActive(true);
    }
    private void Update()
    {
        Mathf.Clamp(currentWeaponIndex, 0, weapons.Count - 1);//限制武器索引范围在0到武器数量-1之间
        if (Input.GetAxis("Mouse ScrollWheel") > 0.1f)
        {
            currentWeaponIndex -= 1;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < -0.1f)
        {
            currentWeaponIndex += 1;
        }
        //ActivateWeapon(currentWeaponIndex);
    }

    private void ActivateWeapon( int index)
    {
        
    }
}
