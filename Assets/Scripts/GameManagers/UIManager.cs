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

    [Header("�Ի���")]
    public GameObject talkPanel;
    public TextMeshProUGUI talkerName;
    public TextMeshProUGUI talkContent;

    private bool isDisplaying = false;// �Ƿ�������ʾ�ı�
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
        talkerName.text = name + "��";
        if (isDisplaying) return; // ���������ʾ�������������

        isDisplaying = true;
        talkContent.text = ""; // ���֮ǰ���ı�
        StartCoroutine(DisplayLinesCoroutine(contents));
    }
    private IEnumerator DisplayLinesCoroutine(string[] contents)
    {
        foreach (string content in contents)
        {

            talkContent.text = content.Trim();// ��ʾ��ǰ���ı�

            // �ȴ�ָ��ʱ�������ʾ��һ��
            yield return new WaitForSeconds(2);
        }
        talkContent.text = "";
        talkFinished = true; // ���öԻ���ɱ�־
        isDisplaying = false;
    }
}
