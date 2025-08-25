using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleNPC : NPCBasic
{
    [Header("��ʹNPC��������")]
    public GameObject specialItem;

    public string[] shopTalkContents;
    private bool shopOpen = false;
    private bool isFirstTalk = true;
    void OnEnable()
    {
        // �����¼���������ʱע�ᣩ
        // ����ǰ��ȡ�����ģ���ֹ�ظ����ģ�
        //EventDefine.OnTalkFinished -= OnTalkFinishedHandler;
        EventDefine.OnTalkFinished += OnTalkFinishedHandler;
    }
    void OnDisable()
    {
        // ȡ�����ģ��ڽ���ʱ�Ƴ�����������ã�
        EventDefine.OnTalkFinished -= OnTalkFinishedHandler;
    }
    protected override void Update()
    {
        base.Update();

        if (isInteract && canTalk)
        {
<<<<<<< HEAD
            UIManager.Instance.SetTalkContent(this,npcName,
                isFirstTalk ? talkContents.ToArray() : shopTalkContents);//�ж��Ƿ���ζԻ������벻ͬ�ĶԻ�����
        }
    }
    private void OnTalkFinishedHandler(NPCBasic sender)
    {
        if (sender != this) return;//ֻ�����Լ����¼�
=======
            UIManager.Instance.SetTalkContent(npcName,
                isFirstTalk ? talkContents.ToArray() : shopTalkContents);//�ж��Ƿ���ζԻ������벻ͬ�ĶԻ�����
        }
    }
    private void OnTalkFinishedHandler()
    {
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
        if (isFirstTalk)
        {
            Instantiate(specialItem, transform.position, Quaternion.identity);
            talkContents.Clear();
            talkContents.AddRange(shopTalkContents);
            isFirstTalk = false; // ���Ϊ���״ζԻ�
        }
        // ���״ζԻ�������ֱ�Ӵ��̵�
        else
        {
            shopOpen = true;
            UIManager.Instance.OpenShopPanel(npcName);
        }
    }
<<<<<<< HEAD
    void OnTriggerStay2D(Collider2D collision)
=======
    void OnTriggerEnter2D(Collider2D collision)
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = true;
            //�����̵�
            if (!isFirstTalk && shopOpen && !UIManager.Instance.shopPanel.activeSelf)
            {
                UIManager.Instance.OpenShopPanel(npcName);
            }
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = false;
            // �뿪ʱ�ر��̵����
            if (shopOpen && UIManager.Instance.shopPanel.activeSelf)
            {
                UIManager.Instance.CloseShopPanel();
            }
        }
    }
<<<<<<< HEAD
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
=======

>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
}