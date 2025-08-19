using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackpack : MonoBehaviour
{
    [Header("�������")]
    public GameObject inventoryPanel; // ��������壨�������и��ӣ�

    private void Start()
    {
        // ��ʼ���ر���
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        // ��B���л�������ʾ״̬
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }
    }

    /// <summary>
    /// �л�������ʾ/����
    /// </summary>
    public void ToggleInventory()
    {
        bool isActive = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isActive);

        // ��ʾ����ʱ��ͣ��Ϸ������ʱ�ָ�
        //Time.timeScale = isActive ? 1 : 0;
    }
}
