using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [SerializeField] private int uid;
    [SerializeField] private int index;
    [SerializeField] private string itemName;
    private GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            ActivateItem();
        }
    }
    private void ActivateItem()
    {
        player.GetComponent<WeaponController>().GetWeaponIndex(index);
    }
}
