using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item")]
public class ShopItem : ScriptableObject
{
    [Header("��Ʒ��Ϣ")]
    public string id;
    public string itemName;
    public string description;
    public int price;
    public int maxStack;
    public ItemType type;
    public Sprite icon;
    public bool isBuyable;      // �Ƿ�ɹ���

    [Header("��ƷЧ��")]
    public int value;
    public GameObject summonObj;

    /// <summary>
    /// ����ScriptableObject�����������⣬��¡һ���µ�ʵ��
    /// </summary>
    /// <returns></returns>
    public ShopItem Clone()
    {
        return new ShopItem()
        {
            id = this.id,
            maxStack = this.maxStack,
            price = this.price,
            type = this.type,
            description = this.description,
            itemName = this.itemName,
            icon = this.icon,
            isBuyable = this.isBuyable,
            value = this.value,
            summonObj = this.summonObj
        };
    }
}
public enum ItemType
{
    Consumable, // ����Ʒ
    Equipment,  // װ��
    Material, //����
    Summon, //�ٻ���
}
[System.Serializable]
public class InventorySlot
{
    public ShopItem item; // ��λ�е���Ʒ��null��ʾ�գ�
    public int count; // ��Ʒ����
}