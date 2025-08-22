using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pausePanel;

    private void OnEnable()
    {
        EventDefine.OnESCPressed += GamePausePanel;
        EventDefine.OnOpenSettings += Hide;
    }
    private void OnDisable()
    {
        EventDefine.OnESCPressed -= GamePausePanel;
        EventDefine.OnOpenSettings -= Hide;
    }
    private void Start()
    {
        if (pausePanel.activeSelf==true)
        {
            pausePanel.SetActive(false);
        }
    }
    public void Hide()
    {
        pausePanel.SetActive(false);
    }
    /// <summary>
    /// 打开或关闭暂停菜单
    /// </summary>
    public void GamePausePanel()
    {
        if (pausePanel.activeSelf == false)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }

    }
}
