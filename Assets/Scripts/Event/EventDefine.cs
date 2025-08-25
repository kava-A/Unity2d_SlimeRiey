using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// ��Ϸ��������ö��
public enum SettingType
{
    FOV,        // ��Ұ
    AudioVolume,// ����
    FrameRate   // ֡��
}

// ���ñ���¼��������ࣨ�����������ͺ�ֵ��
public class SettingChangedEventArgs : EventArgs
{
    public SettingType settingType;
    public float floatValue;       // ����FOV�������ȸ���ֵ
    public int intValue;           // ����֡�ʵ�����ֵ

    // ���캯�����������ʹ��ݲ�ֵͬ��
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
    // �������ñ���¼���Я���������ͺ�ֵ��
    public static event EventHandler<SettingChangedEventArgs> OnSettingChanged;

    // �������ñ���¼�
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
        // ����Ƿ��ж�����
        if (_onESCPressed == null)
        {
            Debug.LogWarning("OnESCPressed �¼�û�ж����ߣ�");
            return;
        }
        _onESCPressed.Invoke(); // ִ���¼�
    }

    private static event Action _onOpenSettings;
    public static event Action OnOpenSettings
    {
        add => _onOpenSettings += value;
        remove => _onOpenSettings -= value;
    }
    public static void CallOpenSettings()
    {
        // ����Ƿ��ж�����
        if (_onOpenSettings == null)
        {
            Debug.LogWarning("OnOpenSettings �¼�û�ж����ߣ�");
            return;
        }
        _onOpenSettings.Invoke(); // ִ���¼�
    }

    private static event Action<NPCBasic> _onTalkFinished;
    public static event Action<NPCBasic> OnTalkFinished
    {
        add => _onTalkFinished += value;
        remove => _onTalkFinished -= value;
    }
    // �ṩ�ڲ�����������������ָ���ഥ����
    public static void CallTriggerTalkFinished(NPCBasic npc)
    {
        // ����Ƿ��ж�����
        if (_onTalkFinished == null)
        {
            Debug.LogWarning("OnTalkFinished �¼�û�ж����ߣ�");
            return;
        }
        _onTalkFinished.Invoke( npc);//ִ���¼�
    }


    // Step1 ������Ʒ�����¼�
    public static event Action<Button> OnBuyItemEvent;
    public static void CallBuyItemEvent(Button clickedButton)
    {
        // ����Ƿ��ж�����
        if (OnBuyItemEvent == null)
        {
            Debug.LogWarning("BuyItemEvent �¼�û�ж����ߣ�");
            return;
        }
        OnBuyItemEvent.Invoke(clickedButton); // ִ���¼�
    }

    //������ʾ��Ʒ�����¼�
    public static event Action<InventoryItem> OnShowDescriptionEvent;
    public static void CallShowDescriptionEvent(InventoryItem clicked)
    {
        // ����Ƿ��ж�����
        if (OnShowDescriptionEvent == null)
        {
            Debug.LogWarning("ShowDescriptionEvent �¼�û�ж����ߣ�");
            return;
        }
        OnShowDescriptionEvent.Invoke(clicked); // ִ���¼�
    }

    //����ʹ����Ʒ�¼�
    public static event Action<InventoryItem> OnUseItemEvent;
    public static void CallUseItemEvent(InventoryItem clicked)
    {
        // ����Ƿ��ж�����
        if (OnUseItemEvent == null)
        {
            Debug.LogWarning("UseItemEvent �¼�û�ж����ߣ�");
            return;
        }
        OnUseItemEvent.Invoke(clicked); // ִ���¼�
    }

    //�������ѡ���¼�
    public static event Action<bool> OnPlayerChooseEvent;
    public static void CallPlayerChooseEvent(bool choose)
    {
        // ����Ƿ��ж�����
        if (OnPlayerChooseEvent == null)
        {
            Debug.LogWarning("PlayerChooseEvent �¼�û�ж����ߣ�");
            return;
        }
        OnPlayerChooseEvent.Invoke(choose); // ִ���¼�
    }
}
