using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [SerializeField] private int uid;
    [SerializeField] private int index;
    [SerializeField] private string itemName;
<<<<<<< HEAD

    [SerializeField] private bool isSpecialWeapon = false; // �Ƿ�Ϊ��������

=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
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
<<<<<<< HEAD
        if (isSpecialWeapon)
        {
            // ������������
            player.GetComponent<WeaponController>().SpcialWeapon();
            return;
        }
        else
        {
            player.GetComponent<WeaponController>().UnlockAndActivateWeapon(index);//������������
        }
=======
        player.GetComponent<WeaponController>().GetWeaponIndex(index);
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    }
}
