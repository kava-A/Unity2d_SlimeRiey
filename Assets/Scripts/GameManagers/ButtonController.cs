using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    /// <summary>
    /// Step2 点击后调用事件方法
    /// </summary>
    /// <param name="clickedButton">点击的按钮对象，作为事件参数传递</param>
    public void OnBuyItemClicked(Button clickedButton)
    {
        Debug.Log("已点击，准备执行事件");
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
