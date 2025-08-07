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

    private bool isDisplaying = false;// 是否正在显示文本
    private bool talkFinished = false;
    public bool TalkFinished {get { return talkFinished; }}
    private void Start()
    {
        if(talkPanel.activeSelf)
        {
            talkPanel.SetActive(false);
        }
        
        talkerName.enabled = false;
        talkContent.enabled = false;
        talkContent.color = talkerName.color;
        talkContent.text = "";
    }
    public void SetTalkContent(string name, string[] contents)
    {
        talkPanel.SetActive(true);
        talkerName.enabled = true;
        talkContent.enabled = true;
        talkerName.text = name + "：";
        if (isDisplaying) return; // 如果正在显示，则忽略新请求

        isDisplaying = true;
        talkContent.text = ""; // 清空之前的文本
        StartCoroutine(DisplayLinesCoroutine(contents));
    }
    private IEnumerator DisplayLinesCoroutine(string[] contents)
    {
        foreach (string content in contents)
        {

            talkContent.text = content.Trim();// 显示当前行文本

            // 等待指定时间后再显示下一行
            yield return new WaitForSeconds(2);
        }
        talkContent.text = "";
        talkFinished = true; // 设置对话完成标志
        isDisplaying = false;
    }
}
