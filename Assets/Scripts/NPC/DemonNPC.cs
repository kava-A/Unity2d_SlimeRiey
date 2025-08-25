using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonNPC : NPCBasic
{
    [Header("恶魔NPC自身属性")]
    public GameObject choosePanel;
    public GameObject item;

    private bool chooseYes = false;//是否接受
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
    /// 对话时间结束开启选择面板
    /// </summary>
    private void OnTalkFinishedHandler(NPCBasic sender)
    {
        if (sender != this) return;

        if (choosePanel != null)
        {
            choosePanel.SetActive(true); // 仅自己对话完成时打开选择面板
        }
    }

    /// <summary>
    /// 玩家选择
    /// </summary>
    private void PlayerChoose(bool value)
    {
        chooseYes = value;
        if(chooseYes)
        {
            if (player != null)
            {
                player.ChangeMaxHealth(2);//改变最大生命值，传入倍率越大，生命值越小
                player.AllStatusM+=1;//所有属性加成倍率加1
                Instantiate(item, transform.position, Quaternion.identity);//生成物品
            }
        }
        else
        {
            Debug.Log("拒绝了");
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
        if(collision.CompareTag("Player")&&!chooseYes)//一旦接受则无法重新对话
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
