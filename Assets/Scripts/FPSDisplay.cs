using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 帧数显示
/// </summary>
public class FPSDisplay : MonoBehaviour
{
    private Text fpsText;

    [SerializeField] private float updateInterval = 0.5f;//更新帧率的频率

    private float _accumulator = 0f;// 帧数累加器
    private int _frameCount = 0;// 帧数计数
    private float _timeLeft;// 距离下次更新的时间
    private float _fps;// 当前帧率
    private void Awake()
    {
        fpsText = GetComponent<Text>();
    }
    private void Start()
    {
        _timeLeft = updateInterval;
    }
    void Update()
    {
        if(Time.timeScale==0)//暂停时不计算帧率
            return;

        _timeLeft -= Time.deltaTime;
        _accumulator += Time.timeScale / Time.deltaTime;
        _frameCount++;

        // 当达到更新间隔时计算FPS
        if (_timeLeft <= 0f)
        {
            // 计算平均帧率
            _fps = _accumulator / _frameCount;
            _timeLeft = updateInterval;
            _accumulator = 0f;
            _frameCount = 0;

            // 更新显示文本，保留一位小数
            fpsText.text = $"FPS: {_fps:0.0}";

            // 根据帧率设置不同颜色，直观显示性能
            if (_fps < 30)
                fpsText.color = Color.yellow;
            else if (_fps < 15)
                fpsText.color = Color.red;
            else
                fpsText.color = Color.green;
        }
    }
}
