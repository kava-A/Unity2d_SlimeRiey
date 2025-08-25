using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        UpdateCoinText();
    }

    private TextMeshProUGUI coinText;

    [Header("杀敌统计")]
    public int killCount = 0; // 杀敌数量
    public int coinCount = 0; // 金币数量

    private void OnEnable()
    {
        EventDefine.OnSettingChanged += ApplySetting;
    }
    private void OnDisable()
    {
        EventDefine.OnSettingChanged -= ApplySetting;
    }
    private void ApplySetting(object sender, SettingChangedEventArgs e)
    {
        switch (e.settingType)
        {
            case SettingType.FOV:
                // 应用视野设置（假设主相机）
                if (Camera.main != null)
                {
                    Camera.main.fieldOfView = e.floatValue;
                    Debug.Log($"FOV已设置为：{e.floatValue}");
                }
                break;

            case SettingType.AudioVolume:
                // 应用音量设置
                AudioListener.volume = e.floatValue;
                // 若有音频混合器，可设置更精细的音量（如对数刻度）
                
                Debug.Log($"音量已设置为：{e.floatValue}");
                break;

            case SettingType.FrameRate:
                // 应用帧率设置（-1表示不限制）
                
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = e.intValue;
                string frameInfo = e.intValue == -1 ? "不限制" : $"{e.intValue}帧";
                Debug.Log($"帧率已设置为：{frameInfo}");
                break;
        }
    }
    private void Update()
    {
        // 每帧检测ESC键是否被按下
        if (Input.GetKeyDown(KeyCode.Escape))
        {
<<<<<<< HEAD
            Debug.Log("ESC键被按下，触发事件");
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
            // 触发ESC事件
            EventDefine.CallTriggerESCPressed();
        }
    }
    public void AddKillCount(int count, int coin)
    {
        killCount += count;
        coinCount += coin;
        UpdateCoinText();
    }
    public void UseCoin(int coin)
    {
        coinCount -= coin;
        
        UpdateCoinText();
    }
    public void UpdateCoinText()
    {
<<<<<<< HEAD
        if(coinText == null) return;
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
        coinText.text = "金币: " + coinCount.ToString();
    }

    public void RegisterCoin(TextMeshProUGUI text)
    {
        coinText = text;
        UpdateCoinText();
    }
}
