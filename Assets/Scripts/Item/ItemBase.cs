using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [SerializeField] private int uid;
    [SerializeField] private int index;
    [SerializeField] private string itemName;

    [SerializeField] private bool isSpecialWeapon = false; // 是否为特殊武器

    private GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            ActivateItem();
            Destroy(gameObject);
        }
    }
    private void ActivateItem()
    {
        if (isSpecialWeapon)
        {
            // 激活特殊武器
            player.GetComponent<WeaponController>().SpcialWeapon();
            return;
        }
        else
        {
            player.GetComponent<WeaponController>().UnlockAndActivateWeapon(index);//常规武器激活
        }
    }
}
