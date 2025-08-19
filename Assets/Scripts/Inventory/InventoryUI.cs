using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [Header("UI����")]
    public GameObject slotPrefab; // �������ӵ�Ԥ����
    public Transform slotParent; // ���ӵĸ����壨�������У�
    private List<GameObject> slotUIList = new List<GameObject>(); // �洢ʵ�����ĸ���

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // ��ʼ��UI���ӣ����ݱ�����λ����������
        InitSlots();
    }

    /// <summary>
    /// ��ʼ����������UI
    /// </summary>
    private void InitSlots()
    {
        // ������и���
        foreach (var slot in slotUIList)
        {
            Destroy(slot);
        }
        slotUIList.Clear();

        // �����¸��ӣ������뱳����λһ�£�
        for (int i = 0; i < InventoryManager.Instance.slotCount; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slotUIList.Add(slot);
            slotParent.gameObject.SetActive(false); // ��ʼʱ���ظ��Ӹ�����
        }
    }

    /// <summary>
    /// ˢ�����и��ӵ���ʾ
    /// </summary>
    /// <param name="slots">�������ݲ�λ</param>
    public void RefreshSlots(List<InventorySlot> slots)
    {
        slotParent.gameObject.SetActive(true); // ��ʾ���Ӹ�����
        for (int i = 0; i < slotUIList.Count; i++)
        {
            GameObject slotUI = slotUIList[i];
            Image icon = slotUI.transform.Find("Icon").GetComponent<Image>(); // ͼ�����
            TextMeshProUGUI countText = slotUI.transform.Find("Count").GetComponent<TextMeshProUGUI>(); // �����ı�
            TextMeshProUGUI nameText= slotUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>(); // �����ı�
            Image bg=slotUI.transform.Find("DescriptionBG").GetComponent<Image>(); // ��������
            TextMeshProUGUI descriptionText = slotUI.transform.Find("DescriptionBG/ItemDescription").GetComponent<TextMeshProUGUI>(); // �����ı�
            TextMeshProUGUI indexText = slotUI.transform.Find("Index").GetComponent<TextMeshProUGUI>(); // �����ı�
            if (i < slots.Count && slots[i].item != null)
            {
                // ��ʾ��Ʒ
                indexText.text = slots[i].item.id;  
                indexText.gameObject.SetActive(false);
                icon.sprite = slots[i].item.icon;
                icon.enabled = true;
                countText.text = slots[i].count > 0 ? slots[i].count.ToString() : ""; // ����>1����ʾ
                countText.enabled = slots[i].count > 0; // ��������0����ʾ�����ı�
                nameText.text = slots[i].item.itemName; // ��ʾ��Ʒ����
                nameText.enabled = true; // ��ʾ�����ı�
                bg.gameObject.SetActive(false); // Ĭ��������������
                descriptionText.text = slots[i].item.description;
                
            }
            else
            {
                // �ղ�λ
                indexText.text = ""; // ��������ı�
                indexText.gameObject.SetActive(false); // ���������ı�
                icon.enabled = false;
                countText.text = "";
                countText.enabled = false;
                nameText.text = ""; // �������
                nameText.enabled = false; // ���������ı�
                descriptionText.text = ""; // ��������ı�
                bg.gameObject.SetActive(false); // ������������
                
            }
        }
    }
}