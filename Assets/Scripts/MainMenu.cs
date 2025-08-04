using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int sceneToLoad;


    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void QuitGame()
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
}
