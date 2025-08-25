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

    [Header("ɱ��ͳ��")]
    public int killCount = 0; // ɱ������
    public int coinCount = 0; // �������

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
                // Ӧ����Ұ���ã������������
                if (Camera.main != null)
                {
                    Camera.main.fieldOfView = e.floatValue;
                    Debug.Log($"FOV������Ϊ��{e.floatValue}");
                }
                break;

            case SettingType.AudioVolume:
                // Ӧ����������
                AudioListener.volume = e.floatValue;
                // ������Ƶ������������ø���ϸ��������������̶ȣ�
                
                Debug.Log($"����������Ϊ��{e.floatValue}");
                break;

            case SettingType.FrameRate:
                // Ӧ��֡�����ã�-1��ʾ�����ƣ�
                
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = e.intValue;
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
<<<<<<< HEAD
            Debug.Log("ESC�������£������¼�");
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
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
<<<<<<< HEAD
        if(coinText == null) return;
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
        coinText.text = "���: " + coinCount.ToString();
    }

    public void RegisterCoin(TextMeshProUGUI text)
    {
        coinText = text;
        UpdateCoinText();
    }
}
