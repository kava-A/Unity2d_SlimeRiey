using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item")]
public class ShopItem : ScriptableObject
{
    [Header("物品信息")]
    public string id;
    public string itemName;
    public string description;
    public int price;
    public int maxStack;
    public ItemType type;
    public Sprite icon;
    public bool isBuyable;      // 是否可购买

    [Header("物品效果")]
    public int value;
    public GameObject summonObj;

    /// <summary>
    /// 避免ScriptableObject引用类型问题，克隆一个新的实例
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
    Consumable, // 消耗品
    Equipment,  // 装备
    Material, //材料
    Summon, //召唤物
}
[System.Serializable]
public class InventorySlot
{
    public ShopItem item; // 槽位中的物品（null表示空）
    public int count; // 物品数量
}