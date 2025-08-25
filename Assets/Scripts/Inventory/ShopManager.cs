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
    [Header("��Ʒ")]
    public ShopItem[] shopItems;
    [Header("�̵갴ť")]
    public Button[] shopButtons;

    private Dictionary<string, ShopItem> itemDict = new Dictionary<string, ShopItem>();
    private void OnEnable()
    {
        EventDefine.OnBuyItemEvent += BuyItem;//Step3 �����¼�
    }
    private void OnDisable()
    {
        EventDefine.OnBuyItemEvent -= BuyItem;//Step3 ȡ�������¼�
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
                    itemName.text=matchItem.itemName+" �ۼۣ�"+matchItem.price.ToString();
                }
            }
        }
    }

    
    /// <summary>
    /// �����¼�����ʵ��Step 1 2 3������ť�����ʱ����
    /// </summary>
    /// <param name="clickedButton">����İ�ť����</param>
    public void BuyItem(Button clickedButton)
    {
     
        ShopButtonBinder binder =clickedButton.GetComponent<ShopButtonBinder>();
        if (binder == null)
        {
            Debug.LogError("��ť��δ����ShopButtonBinder�����");
            return; 
        }

        if (string.IsNullOrEmpty(binder.unitedID))
        {
            Debug.LogError("ShopButtonBinder��unitedIDδ��ֵ��");
            return;
        }
        //����unitedID���Ҷ�Ӧ����Ʒ
        if (itemDict.TryGetValue(binder.unitedID, out ShopItem matchItem))
        {
            int price = matchItem.price;
            if (GameManager.Instance.coinCount >= price)
            {
                Debug.Log("������Ʒ��" + matchItem.name);
                GameManager.Instance.UseCoin(price);
                ShopItem newItemInstance = matchItem.Clone();
                InventoryManager.Instance.AddItem(newItemInstance, 1); // �����ʵ��
            }
            else
            {
                Debug.Log("��Ҳ��㣬�޷�������Ʒ��" + matchItem.name);
            }
        }


    }
}

