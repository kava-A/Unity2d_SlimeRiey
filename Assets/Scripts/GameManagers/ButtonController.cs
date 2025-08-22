using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public void ResumeGame()
    {
        if (Time.timeScale == 0)
        {
            EventDefine.CallTriggerESCPressed();
        }
    }
    /// <summary>
    /// ȷ�����ò��ص���Ϸ
    /// </summary>
    /// <param name="panel"></param>
    public void BackToGame(GameObject panel)
    {
        if(panel.activeSelf==true)
        {
            panel.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void OpenSettingPanel()
    {
        EventDefine.CallOpenSettings();
    }

    public void QuitToMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        // ��WebGL���޷�ֱ���˳�������ת������ҳ�����ʾ��ʾ
        Application.OpenURL("about:blank"); // ����ת��ָ��URL
#else
        Application.Quit();
#endif
        }
        else
        {
            // �������˵�
            Time.timeScale = 1; // ȷ���ڼ����³���ǰ�ָ�ʱ��
            SceneManager.LoadScene("MainMenu");

        }
    }
}
