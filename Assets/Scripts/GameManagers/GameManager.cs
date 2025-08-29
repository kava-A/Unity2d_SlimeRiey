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
    [Header("��ϷĿ��")]
    public int neetToKill = 10;
    public int lifeCount = 3;
    private int maxLifeCount;
    [Header("ɱ��ͳ��")]
    public int killCount = 0; // ɱ������
    public int coinCount = 0; // �������

    private void Start()
    {
        QualitySettings.vSyncCount = 0; // �رմ�ֱͬ��
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
                // Ӧ����Ұ����
                if (Camera.main != null)
                {
                    Camera.main.fieldOfView = e.floatValue;
                    Debug.Log($"FOV������Ϊ��{e.floatValue}");
                }
                break;

            case SettingType.AudioVolume:
                // Ӧ����������
                AudioListener.volume = e.floatValue;


                Debug.Log($"����������Ϊ��{e.floatValue}");
                break;

            case SettingType.FrameRate:
                // Ӧ��֡�����ã�-1��ʾ�����ƣ�

                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = e.intValue;
                Debug.Log($"vSyncCount = {QualitySettings.vSyncCount}");
                string frameInfo = e.intValue == -1 ? "������" : $"{e.intValue}֡";
                Debug.Log($"֡��������Ϊ��{frameInfo}");
                break;
        }
    }
    private void Update()
    {
        // ÿ֡���ESC���Ƿ񱻰���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC�������£������¼�");
            // ����ESC�¼�
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
        coinText.text = "���: " + coinCount.ToString();
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
        if(maxLifeCount <= 0)//�����ľ����������˵�
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
    /// ֹͣ���ɵ���
    /// </summary>
    /// <returns>true�������ɣ�falseֹͣ</returns>
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
