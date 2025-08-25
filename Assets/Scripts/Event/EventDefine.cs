using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 游戏设置类型枚举
public enum SettingType
{
    FOV,        // 视野
    AudioVolume,// 音量
    FrameRate   // 帧率
}

// 设置变更事件的数据类（传递设置类型和值）
public class SettingChangedEventArgs : EventArgs
{
    public SettingType settingType;
    public float floatValue;       // 用于FOV、音量等浮点值
    public int intValue;           // 用于帧率等整数值

    // 构造函数（根据类型传递不同值）
    public SettingChangedEventArgs(SettingType type, float value)
    {
        settingType = type;
        floatValue = value;
    }

    public SettingChangedEventArgs(SettingType type, int value)
    {
        settingType = type;
        intValue = value;
    }
}
public static class EventDefine
{
    // 定义设置变更事件（携带设置类型和值）
    public static event EventHandler<SettingChangedEventArgs> OnSettingChanged;

    // 触发设置变更事件
    public static void TriggerSettingChanged(SettingType type, float value)
    {
        OnSettingChanged?.Invoke(null, new SettingChangedEventArgs(type, value));
    }

    public static void TriggerSettingChanged(SettingType type, int value)
    {
        OnSettingChanged?.Invoke(null, new SettingChangedEventArgs(type, value));
    }

    private static event Action _onESCPressed;
    public static event Action OnESCPressed
    {
        add => _onESCPressed += value;
        remove => _onESCPressed -= value;
    }
    public static void CallTriggerESCPressed()
    {
        // 检查是否有订阅者
        if (_onESCPressed == null)
        {
            Debug.LogWarning("OnESCPressed 事件没有订阅者！");
            return;
        }
        _onESCPressed.Invoke(); // 执行事件
    }

    private static event Action _onOpenSettings;
    public static event Action OnOpenSettings
    {
        add => _onOpenSettings += value;
        remove => _onOpenSettings -= value;
    }
    public static void CallOpenSettings()
    {
        // 检查是否有订阅者
        if (_onOpenSettings == null)
        {
            Debug.LogWarning("OnOpenSettings 事件没有订阅者！");
            return;
        }
        _onOpenSettings.Invoke(); // 执行事件
    }

    private static event Action<NPCBasic> _onTalkFinished;
    public static event Action<NPCBasic> OnTalkFinished
    {
        add => _onTalkFinished += value;
        remove => _onTalkFinished -= value;
    }
    // 提供内部触发方法（仅允许指定类触发）
    public static void CallTriggerTalkFinished(NPCBasic npc)
    {
        // 检查是否有订阅者
        if (_onTalkFinished == null)
        {
            Debug.LogWarning("OnTalkFinished 事件没有订阅者！");
            return;
        }
        _onTalkFinished.Invoke( npc);//执行事件
    }


    // Step1 定义物品购买事件
    public static event Action<Button> OnBuyItemEvent;
    public static void CallBuyItemEvent(Button clickedButton)
    {
        // 检查是否有订阅者
        if (OnBuyItemEvent == null)
        {
            Debug.LogWarning("BuyItemEvent 事件没有订阅者！");
            return;
        }
        OnBuyItemEvent.Invoke(clickedButton); // 执行事件
    }

    //定义显示物品描述事件
    public static event Action<InventoryItem> OnShowDescriptionEvent;
    public static void CallShowDescriptionEvent(InventoryItem clicked)
    {
        // 检查是否有订阅者
        if (OnShowDescriptionEvent == null)
        {
            Debug.LogWarning("ShowDescriptionEvent 事件没有订阅者！");
            return;
        }
        OnShowDescriptionEvent.Invoke(clicked); // 执行事件
    }

    //定义使用物品事件
    public static event Action<InventoryItem> OnUseItemEvent;
    public static void CallUseItemEvent(InventoryItem clicked)
    {
        // 检查是否有订阅者
        if (OnUseItemEvent == null)
        {
            Debug.LogWarning("UseItemEvent 事件没有订阅者！");
            return;
        }
        OnUseItemEvent.Invoke(clicked); // 执行事件
    }

    //定义玩家选择事件
    public static event Action<bool> OnPlayerChooseEvent;
    public static void CallPlayerChooseEvent(bool choose)
    {
        // 检查是否有订阅者
        if (OnPlayerChooseEvent == null)
        {
            Debug.LogWarning("PlayerChooseEvent 事件没有订阅者！");
            return;
        }
        OnPlayerChooseEvent.Invoke(choose); // 执行事件
    }
}
