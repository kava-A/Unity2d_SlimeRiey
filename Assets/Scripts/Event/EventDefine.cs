using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class EventDefine
{
    private static event Action _onTalkFinished;

    public static event Action OnTalkFinished
    {
        add => _onTalkFinished += value;
        remove => _onTalkFinished -= value;
    }

    // 提供内部触发方法（仅允许指定类触发）
    public static void TriggerTalkFinished()
    {
        // 检查是否有订阅者
        if (_onTalkFinished == null)
        {
            Debug.LogWarning("OnTalkFinished 事件没有订阅者！");
            return;
        }
        _onTalkFinished.Invoke();//执行事件
    }


    // Step1 定义物品购买事件
    public static event Action<Button> BuyItemEvent;
    public static void CallBuyItemEvent(Button clickedButton)
    {
        // 检查是否有订阅者
        if (BuyItemEvent == null)
        {
            Debug.LogWarning("BuyItemEvent 事件没有订阅者！");
            return;
        }
        BuyItemEvent.Invoke(clickedButton); // 执行事件
    }

    //定义显示物品描述事件
    public static event Action<InventoryItem> ShowDescriptionEvent;
    public static void CallShowDescriptionEvent(InventoryItem clicked)
    {
        // 检查是否有订阅者
        if (ShowDescriptionEvent == null)
        {
            Debug.LogWarning("ShowDescriptionEvent 事件没有订阅者！");
            return;
        }
        ShowDescriptionEvent.Invoke(clicked); // 执行事件
    }

    //定义使用物品事件
    public static event Action<InventoryItem> UseItemEvent;
    public static void CallUseItemEvent(InventoryItem clicked)
    {
        // 检查是否有订阅者
        if (UseItemEvent == null)
        {
            Debug.LogWarning("UseItemEvent 事件没有订阅者！");
            return;
        }
        UseItemEvent.Invoke(clicked); // 执行事件
    }
}
