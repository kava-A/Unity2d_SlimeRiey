using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    /// <summary>
<<<<<<< HEAD
    /// 购买物品点击
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    /// Step2 点击后调用事件方法
    /// </summary>
    /// <param name="clickedButton">点击的按钮对象，作为事件参数传递</param>
    public void OnBuyItemClicked(Button clickedButton)
    {
        Debug.Log("已点击，准备执行事件");
        EventDefine.CallBuyItemEvent(clickedButton);
    }

<<<<<<< HEAD
    /// <summary>
    /// 物品描述点击
    /// </summary>
    /// <param name="clicked">点击的物品对象</param>
=======

>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    public void OnShowDescriptionClicked(InventoryItem clicked)
    {
        EventDefine.CallShowDescriptionEvent(clicked);
    }
<<<<<<< HEAD
    /// <summary>
    /// 使用物品点击
    /// </summary>
    /// <param name="clicked">物品对象</param>
=======

>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    public void OnUseItemClicked(InventoryItem clicked)
    {
        EventDefine.CallUseItemEvent(clicked);
    }
<<<<<<< HEAD

    /// <summary>
    /// 玩家选择接受
    /// </summary>
    public void OnPlayerChooseAccept()
    {
        EventDefine.CallPlayerChooseEvent(true);
    }
    /// <summary>
    /// 玩家选择拒绝
    /// </summary>
    public void OnplayerChooseRefuse()
    {
        EventDefine.CallPlayerChooseEvent(false);
    }

=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    public void ResumeGame()
    {
        if (Time.timeScale == 0)
        {
            EventDefine.CallTriggerESCPressed();
        }
    }
    /// <summary>
    /// 确认设置并回到游戏
    /// </summary>
    /// <param name="panel"></param>
    public void BackToGame(GameObject panel)
    {
        if(panel.activeSelf==true)
        {
            panel.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void OpenSettingPanel()
    {
        EventDefine.CallOpenSettings();
    }

    public void QuitToMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        // 在WebGL中无法直接退出，可跳转到其他页面或显示提示
        Application.OpenURL("about:blank"); // 或跳转到指定URL
#else
        Application.Quit();
#endif
        }
        else
        {
            // 返回主菜单
            Time.timeScale = 1; // 确保在加载新场景前恢复时间
            SceneManager.LoadScene("MainMenu");

        }
    }
}
