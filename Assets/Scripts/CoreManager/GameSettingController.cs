using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingController : MonoBehaviour
{
    public GameObject settingPanel;
    public Slider fovSlider;
    public Slider audioSlider;
    public TMP_Dropdown frameDropdown;

    private int[] frameRates = { 60, 72, 90,120,144,170, -1 }; // -1��ʾ������
    private void OnEnable()
    {
        EventDefine.OnOpenSettings += OpenSettings;
        
    }
    private void OnDisable()
    {
        EventDefine.OnOpenSettings -= OpenSettings;
    }
    private void Start()
    {
        fovSlider.minValue = 20;
        fovSlider.maxValue = 40;
        audioSlider.minValue = 0;
        audioSlider.maxValue = 100;
        if (settingPanel != null && settingPanel.activeSelf)
        {
            settingPanel.SetActive(false); // ȷ����������ʼʱ�����ص�
        }
        fovSlider.onValueChanged.AddListener(OnFOVChanged);
        audioSlider.onValueChanged.AddListener(OnAudioChanged);
        frameDropdown.onValueChanged.AddListener(OnFrameRateChanged);
        audioSlider.value = 0.5f;
    }

    private void OnFOVChanged(float arg0)
    {
        EventDefine.TriggerSettingChanged(SettingType.FOV, arg0);
    }

    private void OnAudioChanged(float arg0)
    {
        EventDefine.TriggerSettingChanged(SettingType.AudioVolume, arg0); ;
    }

    private void OnFrameRateChanged(int index)
    {
        int frameRate = frameRates[index];
        // ����֡�����ñ���¼�
        EventDefine.TriggerSettingChanged(SettingType.FrameRate, frameRate);
    }

    public void OpenSettings()
    {
        if (Time.timeScale != 0) Time.timeScale = 0; // ��ͣ��Ϸ
        settingPanel.SetActive(true); // ��ʾ�������
    }

    
}
