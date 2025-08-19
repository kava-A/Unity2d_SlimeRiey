using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // ����������ȫ�ֵ���

    [Header("��������")]
    public int slotCount = 20; // �����ܸ�����
    public List<InventorySlot> slots = new List<InventorySlot>(); // ���в�λ

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // ��ʼ��������λ��ȫ��Ϊ�գ�
        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(new InventorySlot());
        }
        UpdateUI();
    }

    /// <summary>
    /// �򱳰������Ʒ
    /// </summary>
    /// <param name="newItem">Ҫ��ӵ���Ʒ</param>
    /// <param name="amount">�������</param>
    /// <returns>�Ƿ���ӳɹ�</returns>
    public bool AddItem(ShopItem newItem, int amount = 1)
    {
        if (newItem == null || amount <= 0) return false;

        // 1. �ȳ��Զѵ���������ͬ��Ʒ�Ĳ�λ
        foreach (var slot in slots)
        {
            // ��λ����ͬ��Ʒ����δ�����ѵ�
            if (slot.item != null)
            {
                Debug.Log($"���ѵ���������ƷID={slot.item.id}������ƷID={newItem.id}");
            }
            // ԭ���ж��߼�
            if (slot.item != null && slot.item.id == newItem.id && slot.count < newItem.maxStack)
            {
                int addable = Mathf.Min(amount, newItem.maxStack - slot.count);
                slot.count += addable;
                amount -= addable;

                if (amount == 0) // ȫ���ѵ����
                {
                    UpdateUI(); // ˢ�±���UI
                    return true;
                }
            }
        }

        // 2. ��ͬID����Ʒ��Ѱ�ҿղ�λ
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

        // 3. û�пղ�λ�����ʧ��
        Debug.LogWarning("�����������޷������Ʒ��");
        return false;
    }

    /// <summary>
    /// ֪ͨUIˢ�±�����ʾ
    /// </summary>
    private void UpdateUI()
    {
        if (InventoryUI.Instance != null)
        {
            InventoryUI.Instance.RefreshSlots(slots);
        }
    }
}