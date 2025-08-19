using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // 单例，方便全局调用

    [Header("背包配置")]
    public int slotCount = 20; // 背包总格子数
    public List<InventorySlot> slots = new List<InventorySlot>(); // 所有槽位

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // 初始化背包槽位（全部为空）
        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(new InventorySlot());
        }
        UpdateUI();
    }

    /// <summary>
    /// 向背包添加物品
    /// </summary>
    /// <param name="newItem">要添加的物品</param>
    /// <param name="amount">添加数量</param>
    /// <returns>是否添加成功</returns>
    public bool AddItem(ShopItem newItem, int amount = 1)
    {
        if (newItem == null || amount <= 0) return false;

        // 1. 先尝试堆叠到已有相同物品的槽位
        foreach (var slot in slots)
        {
            // 槽位有相同物品，且未达最大堆叠
            if (slot.item != null)
            {
                Debug.Log($"检查堆叠：现有物品ID={slot.item.id}，新物品ID={newItem.id}");
            }
            // 原有判断逻辑
            if (slot.item != null && slot.item.id == newItem.id && slot.count < newItem.maxStack)
            {
                int addable = Mathf.Min(amount, newItem.maxStack - slot.count);
                slot.count += addable;
                amount -= addable;

                if (amount == 0) // 全部堆叠完成
                {
                    UpdateUI(); // 刷新背包UI
                    return true;
                }
            }
        }

        // 2. 不同ID的物品，寻找空槽位
        foreach (var slot in slots)
        {
            if (slot.item == null)
            {
                slot.item = newItem;
                slot.count = amount;
                UpdateUI();
                return true;
            }
        }

        // 3. 没有空槽位，添加失败
        Debug.LogWarning("背包已满，无法添加物品！");
        return false;
    }

    /// <summary>
    /// 通知UI刷新背包显示
    /// </summary>
    private void UpdateUI()
    {
        if (InventoryUI.Instance != null)
        {
            InventoryUI.Instance.RefreshSlots(slots);
        }
    }
}