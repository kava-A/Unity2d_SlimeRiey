using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackpack : MonoBehaviour
{
    [Header("背包面板")]
    public GameObject inventoryPanel; // 背包主面板（包含所有格子）

    private void Start()
    {
        // 初始隐藏背包
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        // 按B键切换背包显示状态
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventory();
        }
    }

    /// <summary>
    /// 切换背包显示/隐藏
    /// </summary>
    public void ToggleInventory()
    {
        bool isActive = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isActive);

        // 显示背包时暂停游戏，隐藏时恢复
        //Time.timeScale = isActive ? 1 : 0;
    }
}
