using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    /// <summary>
    /// Step2 ���������¼�����
    /// </summary>
    /// <param name="clickedButton">����İ�ť������Ϊ�¼���������</param>
    public void OnBuyItemClicked(Button clickedButton)
    {
        Debug.Log("�ѵ����׼��ִ���¼�");
        EventDefine.CallBuyItemEvent(clickedButton);
    }


    public void OnShowDescriptionClicked(InventoryItem clicked)
    {
        EventDefine.CallShowDescriptionEvent(clicked);
    }

    public void OnUseItemClicked(InventoryItem clicked)
    {
        EventDefine.CallUseItemEvent(clicked);
    }
}
