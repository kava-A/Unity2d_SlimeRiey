using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonNPC : NPCBasic
{
    [Header("��ħNPC��������")]
    public GameObject choosePanel;
    public GameObject item;

    private bool chooseYes = false;//�Ƿ����
    private PlayerStatus player;
    private void OnEnable()
    {
        EventDefine.OnTalkFinished += OnTalkFinishedHandler;
        EventDefine.OnPlayerChooseEvent += PlayerChoose;
    }
    private void OnDisable()
    {
        EventDefine.OnTalkFinished -= OnTalkFinishedHandler;
        EventDefine.OnPlayerChooseEvent -= PlayerChoose;
    }
    /// <summary>
    /// �Ի�ʱ���������ѡ�����
    /// </summary>
    private void OnTalkFinishedHandler(NPCBasic sender)
    {
        if (sender != this) return;

        if (choosePanel != null)
        {
            choosePanel.SetActive(true); // ���Լ��Ի����ʱ��ѡ�����
        }
    }

    /// <summary>
    /// ���ѡ��
    /// </summary>
    private void PlayerChoose(bool value)
    {
        chooseYes = value;
        if(chooseYes)
        {
            if (player != null)
            {
                player.ChangeMaxHealth(2);//�ı��������ֵ�����뱶��Խ������ֵԽС
                player.AllStatusM+=1;//�������Լӳɱ��ʼ�1
                Instantiate(item, transform.position, Quaternion.identity);//������Ʒ
            }
        }
        else
        {
            Debug.Log("�ܾ���");
        }
       
        choosePanel.SetActive(false);
        Destroy(gameObject);
    }
    protected override void Update()
    {
        base.Update();
        if (isInteract&&canTalk)
        {
            UIManager.Instance.SetTalkContent(this,npcName, talkContents.ToArray());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&&!chooseYes)//һ���������޷����¶Ի�
        {
            player = collision.GetComponent<PlayerStatus>();
            canTalk = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canTalk = false;
        }
    }
}
