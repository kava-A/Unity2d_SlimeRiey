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
        // 在WebGL中无法直接退出，可跳转到其他页面或显示提示
        Application.OpenURL("about:blank"); // 或跳转到指定URL
#else
        Application.Quit();
#endif
    }
}
