using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 玩家使用物品逻辑
/// </summary>
public class InventoryItem : MonoBehaviour
{
    public GameObject descriptionObj;
    public ShopItem[] shopItems; // 商店物品数组
    private GameObject player; // 玩家对象
    private void Awake()
    {
        descriptionObj.SetActive(false); // 初始隐藏描述对象
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnEnable()
    {
        EventDefine.ShowDescriptionEvent += ShowDescription;
        EventDefine.UseItemEvent += UseItem; // 订阅使用物品事件
    }
    private void Start()
    {
        
    }
    private void OnDisable()
    {
        EventDefine.ShowDescriptionEvent -= ShowDescription;
        EventDefine.UseItemEvent -= UseItem; // 取消订阅使用物品事件
    }

    /// <summary>
    /// 执行事件逻辑
    /// </summary>
    private void ShowDescription(InventoryItem clicked)
    {
        if (this == clicked)
        {
            // 显示当前物品的描述
            bool isShow = descriptionObj.activeSelf;
            descriptionObj.SetActive(!isShow);
        }
        else
        {
            // 隐藏其他物品的描述
            descriptionObj.SetActive(false);
        }
    }

    private void UseItem(InventoryItem clicked)
    {
        //需要确认点击的物品是当前物品
        if (this == clicked)
        {
            TextMeshProUGUI index = clicked.transform.Find("Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI count = clicked.transform.Find("Count").GetComponent<TextMeshProUGUI>();
            if(int.Parse(count.text)<1)
            {
                Debug.Log("已使用完该物品");
                return;
            }
            foreach (ShopItem item in shopItems)
            {
                if (item.id == index.text)
                {
                    switch (item.type)
                    {
                        case ItemType.Consumable:
                            ConsumableItem(item);
                            count.text = (int.Parse(count.text) - 1).ToString(); // 使用后数量减1
                            break;
                        case ItemType.Equipment:
                            break;
                        case ItemType.Material:
                            break;
                        case ItemType.Summon:
                            SummonItem(item); 
                            count.text = (int.Parse(count.text) - 1).ToString(); // 使用后数量减1
                            break;
                        default:
                            Debug.LogWarning("未知物品类型");
                            break;
                    }
                    return;
                }
            }
        }

    }


    private void ConsumableItem(ShopItem item)
    {
        PlayerStatus status = player.GetComponent<PlayerStatus>();
        if (status != null)
        {
            status.Heal(item.value);
        }


    }
    /// <summary>
    /// 召唤物品
    /// </summary>
    /// <param name="item"></param>
    private void SummonItem(ShopItem item)
    {
        GameObject gameObject = item.summonObj;
        Vector3 offset = new Vector3(5, 0,0); // 偏移量
        gameObject = Instantiate(gameObject, player.transform.position+offset, Quaternion.identity);
    }
}
