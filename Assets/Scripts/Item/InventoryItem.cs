using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���ʹ����Ʒ�߼�
/// </summary>
public class InventoryItem : MonoBehaviour
{
    public GameObject descriptionObj;
    public ShopItem[] shopItems; // �̵���Ʒ����
    private GameObject player; // ��Ҷ���
    private void Awake()
    {
        descriptionObj.SetActive(false); // ��ʼ������������
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnEnable()
    {
        EventDefine.ShowDescriptionEvent += ShowDescription;
        EventDefine.UseItemEvent += UseItem; // ����ʹ����Ʒ�¼�
    }
    private void Start()
    {
        
    }
    private void OnDisable()
    {
        EventDefine.ShowDescriptionEvent -= ShowDescription;
        EventDefine.UseItemEvent -= UseItem; // ȡ������ʹ����Ʒ�¼�
    }

    /// <summary>
    /// ִ���¼��߼�
    /// </summary>
    private void ShowDescription(InventoryItem clicked)
    {
        if (this == clicked)
        {
            // ��ʾ��ǰ��Ʒ������
            bool isShow = descriptionObj.activeSelf;
            descriptionObj.SetActive(!isShow);
        }
        else
        {
            // ����������Ʒ������
            descriptionObj.SetActive(false);
        }
    }

    private void UseItem(InventoryItem clicked)
    {
        //��Ҫȷ�ϵ������Ʒ�ǵ�ǰ��Ʒ
        if (this == clicked)
        {
            TextMeshProUGUI index = clicked.transform.Find("Index").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI count = clicked.transform.Find("Count").GetComponent<TextMeshProUGUI>();
            if(int.Parse(count.text)<1)
            {
                Debug.Log("��ʹ�������Ʒ");
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
                            count.text = (int.Parse(count.text) - 1).ToString(); // ʹ�ú�������1
                            break;
                        case ItemType.Equipment:
                            break;
                        case ItemType.Material:
                            break;
                        case ItemType.Summon:
                            SummonItem(item); 
                            count.text = (int.Parse(count.text) - 1).ToString(); // ʹ�ú�������1
                            break;
                        default:
                            Debug.LogWarning("δ֪��Ʒ����");
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
    /// �ٻ���Ʒ
    /// </summary>
    /// <param name="item"></param>
    private void SummonItem(ShopItem item)
    {
        GameObject gameObject = item.summonObj;
        Vector3 offset = new Vector3(5, 0,0); // ƫ����
        gameObject = Instantiate(gameObject, player.transform.position+offset, Quaternion.identity);
    }
}
