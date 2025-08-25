using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instatce;

    private void Awake()
    {
        if (Instatce == null)
        {
            Instatce = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [Header("物品")]
    public ShopItem[] shopItems;
    [Header("商店按钮")]
    public Button[] shopButtons;

    private Dictionary<string, ShopItem> itemDict = new Dictionary<string, ShopItem>();
    private void OnEnable()
    {
        EventDefine.OnBuyItemEvent += BuyItem;//Step3 订阅事件
    }
    private void OnDisable()
    {
        EventDefine.OnBuyItemEvent -= BuyItem;//Step3 取消订阅事件
    }
    private void Start()
    {
        foreach (var item in shopItems)
        {
            if (itemDict.ContainsKey(item.id))
            {
                continue;
            }

            itemDict.Add(item.id, item);
        }
        
        foreach (var button in shopButtons)
        {
            
            ShopButtonBinder shopButtonBinder = button.GetComponent<ShopButtonBinder>();
            if (shopButtonBinder == null || string.IsNullOrEmpty(shopButtonBinder.unitedID))
            {
                continue;
            }

            if (itemDict.TryGetValue(shopButtonBinder.unitedID, out ShopItem matchItem))
            {
                Image buttonImage = button.GetComponent<Image>();
                TextMeshProUGUI itemName=button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonImage != null)
                {
                    buttonImage.sprite = matchItem.icon;
                    itemName.text=matchItem.itemName+" 售价："+matchItem.price.ToString();
                }
            }
        }
    }

    
    /// <summary>
    /// 购买事件，已实现Step 1 2 3，当按钮被点击时触发
    /// </summary>
    /// <param name="clickedButton">点击的按钮对象</param>
    public void BuyItem(Button clickedButton)
    {
     
        ShopButtonBinder binder =clickedButton.GetComponent<ShopButtonBinder>();
        if (binder == null)
        {
            Debug.LogError("按钮上未挂载ShopButtonBinder组件！");
            return; 
        }

        if (string.IsNullOrEmpty(binder.unitedID))
        {
            Debug.LogError("ShopButtonBinder的unitedID未赋值！");
            return;
        }
        //根据unitedID查找对应的物品
        if (itemDict.TryGetValue(binder.unitedID, out ShopItem matchItem))
        {
            int price = matchItem.price;
            if (GameManager.Instance.coinCount >= price)
            {
                Debug.Log("购买物品：" + matchItem.name);
                GameManager.Instance.UseCoin(price);
                ShopItem newItemInstance = matchItem.Clone();
                InventoryManager.Instance.AddItem(newItemInstance, 1); // 添加新实例
            }
            else
            {
                Debug.Log("金币不足，无法购买物品：" + matchItem.name);
            }
        }


    }
}

