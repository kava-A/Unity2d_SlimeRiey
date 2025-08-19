using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("对话框")]
    public GameObject talkPanel;
    public TextMeshProUGUI talkerName;
    public TextMeshProUGUI talkContent;
    [Header("商店")]
    public GameObject shopPanel;

    private Coroutine talkCoroutine; // 保存协程引用

    private bool isDisplaying = false;// 是否正在显示文本
    private bool talkFinished = false;
    public bool TalkFinished {
        get => talkFinished;
        set //只有通过属性修改时才能触发事件
        {
            talkFinished = value;
            if (talkFinished)
            {
                // 触发事件
                EventDefine.TriggerTalkFinished();
            }
        }
    }
    private void Start()
    {
        if(talkPanel.activeSelf)
        {
            talkPanel.SetActive(false);
        }
        shopPanel.SetActive(false);
        talkerName.enabled = false;
        talkContent.enabled = false;
        talkContent.color = talkerName.color;
        talkContent.text = "";
    }
    public void SetTalkContent(string name, string[] contents)
    {
        if (talkPanel == null || talkerName == null || talkContent == null)
        {
            Debug.LogError("UI组件未赋值！");
            return;
        }

        talkFinished = false; // 重置对话完成状态
        isDisplaying = false;
        talkPanel.SetActive(true);
        talkerName.enabled = true;
        talkContent.enabled = true;
        talkerName.text = name + "：";
        if (isDisplaying) return; // 如果正在显示，则忽略新请求

        isDisplaying = true;
        talkContent.text = ""; // 清空之前的文本
        StartCoroutine(DisplayLinesCoroutine(contents));

        if (talkCoroutine != null)
            StopCoroutine(talkCoroutine); // 先停止之前的协程
        talkCoroutine = StartCoroutine(DisplayLinesCoroutine(contents));
    }
    private IEnumerator DisplayLinesCoroutine(string[] contents)
    {
        foreach (string content in contents)
        {
            talkContent.text = content;
            // 等待2秒 或 玩家按下Q
            float waitTime = 0;
            while (waitTime < 2 && !Input.GetKeyDown(KeyCode.Q))
            {
                waitTime += Time.deltaTime;
                yield return null;
            }
        }
        talkContent.text = "";
        if (!talkFinished) // 仅在状态变化时触发
        {
            TalkFinished = true;
        }
        isDisplaying = false;
        talkPanel.SetActive(false); // 隐藏对话框
    }

    public void OpenShopPanel(string name)
    {
        
        shopPanel.SetActive(true);
    }
    public void CloseShopPanel()
    {
        shopPanel.SetActive(false);
        talkFinished = false; // 重置对话完成标志
        talkerName.enabled = false;
        talkContent.enabled = false;
        talkContent.text = "";
    }
}
