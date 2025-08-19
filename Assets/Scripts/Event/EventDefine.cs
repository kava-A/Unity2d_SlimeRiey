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

    // �ṩ�ڲ�����������������ָ���ഥ����
    public static void TriggerTalkFinished()
    {
        // ����Ƿ��ж�����
        if (_onTalkFinished == null)
        {
            Debug.LogWarning("OnTalkFinished �¼�û�ж����ߣ�");
            return;
        }
        _onTalkFinished.Invoke();//ִ���¼�
    }


    // Step1 ������Ʒ�����¼�
    public static event Action<Button> BuyItemEvent;
    public static void CallBuyItemEvent(Button clickedButton)
    {
        // ����Ƿ��ж�����
        if (BuyItemEvent == null)
        {
            Debug.LogWarning("BuyItemEvent �¼�û�ж����ߣ�");
            return;
        }
        BuyItemEvent.Invoke(clickedButton); // ִ���¼�
    }

    //������ʾ��Ʒ�����¼�
    public static event Action<InventoryItem> ShowDescriptionEvent;
    public static void CallShowDescriptionEvent(InventoryItem clicked)
    {
        // ����Ƿ��ж�����
        if (ShowDescriptionEvent == null)
        {
            Debug.LogWarning("ShowDescriptionEvent �¼�û�ж����ߣ�");
            return;
        }
        ShowDescriptionEvent.Invoke(clicked); // ִ���¼�
    }

    //����ʹ����Ʒ�¼�
    public static event Action<InventoryItem> UseItemEvent;
    public static void CallUseItemEvent(InventoryItem clicked)
    {
        // ����Ƿ��ж�����
        if (UseItemEvent == null)
        {
            Debug.LogWarning("UseItemEvent �¼�û�ж����ߣ�");
            return;
        }
        UseItemEvent.Invoke(clicked); // ִ���¼�
    }
}
