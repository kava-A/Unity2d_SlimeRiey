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
<<<<<<< HEAD
    private NPCBasic currentTalkingNPC;//��¼˵��NPC
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892

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
<<<<<<< HEAD
                EventDefine.CallTriggerTalkFinished(currentTalkingNPC);
=======
                EventDefine.CallTriggerTalkFinished();
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
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
<<<<<<< HEAD
    public void SetTalkContent(NPCBasic npc,string name, string[] contents)
    {
        currentTalkingNPC = npc;
=======
    public void SetTalkContent(string name, string[] contents)
    {
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
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
<<<<<<< HEAD
            EventDefine.CallTriggerTalkFinished(currentTalkingNPC);
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
        }
        isDisplaying = false;
        talkPanel.SetActive(false); // ���ضԻ���
    }

    public void OpenShopPanel(string name)
    {
<<<<<<< HEAD
=======
        
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
        shopPanel.SetActive(true);
    }
    public void CloseShopPanel()
    {
        shopPanel.SetActive(false);
        talkFinished = false; // ���öԻ���ɱ�־
        talkerName.enabled = false;
        talkContent.enabled = false;
        talkContent.text = "";
<<<<<<< HEAD
        StopCoroutine(DisplayLinesCoroutine(new string[] { }));//ֹͣ��ʾ�ı�Э��
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    }
}
