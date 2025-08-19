using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [Header("UI配置")]
    public GameObject slotPrefab; // 单个格子的预制体
    public Transform slotParent; // 格子的父物体（用于排列）
    private List<GameObject> slotUIList = new List<GameObject>(); // 存储实例化的格子

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // 初始化UI格子（根据背包槽位数量创建）
        InitSlots();
    }

    /// <summary>
    /// 初始化背包格子UI
    /// </summary>
    private void InitSlots()
    {
        // 清除已有格子
        foreach (var slot in slotUIList)
        {
            Destroy(slot);
        }
        slotUIList.Clear();

        // 创建新格子（数量与背包槽位一致）
        for (int i = 0; i < InventoryManager.Instance.slotCount; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slotUIList.Add(slot);
            slotParent.gameObject.SetActive(false); // 初始时隐藏格子父物体
        }
    }

    /// <summary>
    /// 刷新所有格子的显示
    /// </summary>
    /// <param name="slots">背包数据槽位</param>
    public void RefreshSlots(List<InventorySlot> slots)
    {
        slotParent.gameObject.SetActive(true); // 显示格子父物体
        for (int i = 0; i < slotUIList.Count; i++)
        {
            GameObject slotUI = slotUIList[i];
            Image icon = slotUI.transform.Find("Icon").GetComponent<Image>(); // 图标组件
            TextMeshProUGUI countText = slotUI.transform.Find("Count").GetComponent<TextMeshProUGUI>(); // 数量文本
            TextMeshProUGUI nameText= slotUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>(); // 名称文本
            Image bg=slotUI.transform.Find("DescriptionBG").GetComponent<Image>(); // 描述背景
            TextMeshProUGUI descriptionText = slotUI.transform.Find("DescriptionBG/ItemDescription").GetComponent<TextMeshProUGUI>(); // 描述文本
            TextMeshProUGUI indexText = slotUI.transform.Find("Index").GetComponent<TextMeshProUGUI>(); // 索引文本
            if (i < slots.Count && slots[i].item != null)
            {
                // 显示物品
                indexText.text = slots[i].item.id;  
                indexText.gameObject.SetActive(false);
                icon.sprite = slots[i].item.icon;
                icon.enabled = true;
                countText.text = slots[i].count > 0 ? slots[i].count.ToString() : ""; // 数量>1才显示
                countText.enabled = slots[i].count > 0; // 数量大于0才显示数量文本
                nameText.text = slots[i].item.itemName; // 显示物品名称
                nameText.enabled = true; // 显示名称文本
                bg.gameObject.SetActive(false); // 默认隐藏描述背景
                descriptionText.text = slots[i].item.description;
                
            }
            else
            {
                // 空槽位
                indexText.text = ""; // 清空索引文本
                indexText.gameObject.SetActive(false); // 隐藏索引文本
                icon.enabled = false;
                countText.text = "";
                countText.enabled = false;
                nameText.text = ""; // 清空名称
                nameText.enabled = false; // 隐藏名称文本
                descriptionText.text = ""; // 清空描述文本
                bg.gameObject.SetActive(false); // 隐藏描述背景
                
            }
        }
    }
}