using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    /// <summary>
<<<<<<< HEAD
    /// ������Ʒ���
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    /// Step2 ���������¼�����
    /// </summary>
    /// <param name="clickedButton">����İ�ť������Ϊ�¼���������</param>
    public void OnBuyItemClicked(Button clickedButton)
    {
        Debug.Log("�ѵ����׼��ִ���¼�");
        EventDefine.CallBuyItemEvent(clickedButton);
    }

<<<<<<< HEAD
    /// <summary>
    /// ��Ʒ�������
    /// </summary>
    /// <param name="clicked">�������Ʒ����</param>
=======

>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    public void OnShowDescriptionClicked(InventoryItem clicked)
    {
        EventDefine.CallShowDescriptionEvent(clicked);
    }
<<<<<<< HEAD
    /// <summary>
    /// ʹ����Ʒ���
    /// </summary>
    /// <param name="clicked">��Ʒ����</param>
=======

>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    public void OnUseItemClicked(InventoryItem clicked)
    {
        EventDefine.CallUseItemEvent(clicked);
    }
<<<<<<< HEAD

    /// <summary>
    /// ���ѡ�����
    /// </summary>
    public void OnPlayerChooseAccept()
    {
        EventDefine.CallPlayerChooseEvent(true);
    }
    /// <summary>
    /// ���ѡ��ܾ�
    /// </summary>
    public void OnplayerChooseRefuse()
    {
        EventDefine.CallPlayerChooseEvent(false);
    }

=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
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
