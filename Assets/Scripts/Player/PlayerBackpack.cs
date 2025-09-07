using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerBackpack : MonoBehaviourPun
{
    [Header("背包面板")]
    public GameObject inventoryPanel; // 背包主面板（包含所有格子）

    private void Start()
    {
        if (!photonView.IsMine) return;

        // 通过单例获取，无需依赖路径
        if (UIManager.Instance != null)
        {
            inventoryPanel = UIManager.Instance.backpackPanel;
            if (inventoryPanel != null)
            {
                inventoryPanel.SetActive(false); // 初始隐藏
            }
            else
            {
                Debug.LogError("UIManager中未赋值背包面板！");
            }
        }
        else
        {
            Debug.LogError("场景中未找到UIManager实例！");
        }
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
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
