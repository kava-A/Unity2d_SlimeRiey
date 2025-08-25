using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleNPC : NPCBasic
{
    [Header("天使NPC自身属性")]
    public GameObject specialItem;

    public string[] shopTalkContents;
    private bool shopOpen = false;
    private bool isFirstTalk = true;
    void OnEnable()
    {
        // 订阅事件（在启用时注册）
        // 订阅前先取消订阅（防止重复订阅）
        //EventDefine.OnTalkFinished -= OnTalkFinishedHandler;
        EventDefine.OnTalkFinished += OnTalkFinishedHandler;
    }
    void OnDisable()
    {
        // 取消订阅（在禁用时移除，避免空引用）
        EventDefine.OnTalkFinished -= OnTalkFinishedHandler;
    }
    protected override void Update()
    {
        base.Update();

        if (isInteract && canTalk)
        {
            UIManager.Instance.SetTalkContent(this,npcName,
                isFirstTalk ? talkContents.ToArray() : shopTalkContents);//判断是否初次对话，传入不同的对话内容
        }
    }
    private void OnTalkFinishedHandler(NPCBasic sender)
    {
        if (sender != this) return;//只处理自己的事件
        if (isFirstTalk)
        {
            Instantiate(specialItem, transform.position, Quaternion.identity);
            talkContents.Clear();
            talkContents.AddRange(shopTalkContents);
            isFirstTalk = false; // 标记为非首次对话
        }
        // 非首次对话结束：直接打开商店
        else
        {
            shopOpen = true;
            UIManager.Instance.OpenShopPanel(npcName);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = true;
            //开启商店
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
            // 离开时关闭商店面板
            if (shopOpen && UIManager.Instance.shopPanel.activeSelf)
            {
                UIManager.Instance.CloseShopPanel();
            }
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}