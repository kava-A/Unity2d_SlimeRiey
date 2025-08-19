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
    [Header("�̵�")]
    public GameObject shopPanel;

    private Coroutine talkCoroutine; // ����Э������

    private bool isDisplaying = false;// �Ƿ�������ʾ�ı�
    private bool talkFinished = false;
    public bool TalkFinished {
        get => talkFinished;
        set //ֻ��ͨ�������޸�ʱ���ܴ����¼�
        {
            talkFinished = value;
            if (talkFinished)
            {
                // �����¼�
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
            Debug.LogError("UI���δ��ֵ��");
            return;
        }

        talkFinished = false; // ���öԻ����״̬
        isDisplaying = false;
        talkPanel.SetActive(true);
        talkerName.enabled = true;
        talkContent.enabled = true;
        talkerName.text = name + "��";
        if (isDisplaying) return; // ���������ʾ�������������

        isDisplaying = true;
        talkContent.text = ""; // ���֮ǰ���ı�
        StartCoroutine(DisplayLinesCoroutine(contents));

        if (talkCoroutine != null)
            StopCoroutine(talkCoroutine); // ��ֹ֮ͣǰ��Э��
        talkCoroutine = StartCoroutine(DisplayLinesCoroutine(contents));
    }
    private IEnumerator DisplayLinesCoroutine(string[] contents)
    {
        foreach (string content in contents)
        {
            talkContent.text = content;
            // �ȴ�2�� �� ��Ұ���Q
            float waitTime = 0;
            while (waitTime < 2 && !Input.GetKeyDown(KeyCode.Q))
            {
                waitTime += Time.deltaTime;
                yield return null;
            }
        }
        talkContent.text = "";
        if (!talkFinished) // ����״̬�仯ʱ����
        {
            TalkFinished = true;
        }
        isDisplaying = false;
        talkPanel.SetActive(false); // ���ضԻ���
    }

    public void OpenShopPanel(string name)
    {
        
        shopPanel.SetActive(true);
    }
    public void CloseShopPanel()
    {
        shopPanel.SetActive(false);
        talkFinished = false; // ���öԻ���ɱ�־
        talkerName.enabled = false;
        talkContent.enabled = false;
        talkContent.text = "";
    }
}
