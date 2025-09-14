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
        // 在WebGL中无法直接退出，跳转到其他页面
        Application.OpenURL("www.baidu.com"); 
#else
        Application.Quit();
#endif
    }
}
