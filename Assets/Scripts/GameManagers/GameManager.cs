using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

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
    }

    private TextMeshProUGUI coinText;
    private TextMeshProUGUI lifeText;
    [Header("游戏目标")]
    public int neetToKill = 10;
    public int lifeCount = 3;
    private int maxLifeCount;
    [Header("杀敌统计")]
    public int killCount = 0; // 杀敌数量
    public int coinCount = 0; // 金币数量

    private void Start()
    {
        QualitySettings.vSyncCount = 0; // 关闭垂直同步
        Application.targetFrameRate = 60;
        maxLifeCount = lifeCount;
        UpdateLifeText();
        UpdateCoinText();
    }
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
                // 应用视野设置
                if (Camera.main != null)
                {
                    Camera.main.fieldOfView = e.floatValue;
                    Debug.Log($"FOV已设置为：{e.floatValue}");
                }
                break;

            case SettingType.AudioVolume:
                // 应用音量设置
                AudioListener.volume = e.floatValue;


                Debug.Log($"音量已设置为：{e.floatValue}");
                break;

            case SettingType.FrameRate:
                // 应用帧率设置（-1表示不限制）

                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = e.intValue;
                Debug.Log($"vSyncCount = {QualitySettings.vSyncCount}");
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
            Debug.Log("ESC键被按下，触发事件");
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
        if (coinText == null) return;
        coinText.text = "金币: " + coinCount.ToString();
    }
    public void RegisterCoin(TextMeshProUGUI text)
    {
        coinText = text;
        UpdateCoinText();
    }
    public void UpdateLifeText()
    {
        if(lifeText == null) return;
        lifeText.text= "x" +maxLifeCount.ToString();
    }
    public void RegisterLifeText(TextMeshProUGUI text)
    {
        lifeText = text;
        UpdateLifeText();
    }
    public void DeLifeCount(int count)
    {
        maxLifeCount -= count;
        UpdateLifeText() ;
        if(maxLifeCount <= 0)//生命耗尽，返回主菜单
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(0);
        }
    }
    public void AddLifeCount(int count, int? cost)
    {
        if (cost.HasValue)
        {
            if (cost < coinCount)
            {
                coinCount -= cost.Value;
                maxLifeCount += count;
                UpdateLifeText();
            }
        }
        else
        {
            maxLifeCount += count;
            UpdateLifeText();
        }

    }
    /// <summary>
    /// 停止生成敌人
    /// </summary>
    /// <returns>true继续生成，false停止</returns>
    public bool StopSpawnEnemy()
    {
        if (maxLifeCount < 0) return false;
        if (killCount > neetToKill)
        {
            return false;
        }
        return true;
    }
}
