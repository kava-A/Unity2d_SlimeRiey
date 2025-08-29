using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ֡����ʾ
/// </summary>
public class FPSDisplay : MonoBehaviour
{
    private Text fpsText;

    [SerializeField] private float updateInterval = 0.5f;//����֡�ʵ�Ƶ��

    private float _accumulator = 0f;// ֡���ۼ���
    private int _frameCount = 0;// ֡������
    private float _timeLeft;// �����´θ��µ�ʱ��
    private float _fps;// ��ǰ֡��
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
        if(Time.timeScale==0)//��ͣʱ������֡��
            return;

        _timeLeft -= Time.deltaTime;
        _accumulator += Time.timeScale / Time.deltaTime;
        _frameCount++;

        // ���ﵽ���¼��ʱ����FPS
        if (_timeLeft <= 0f)
        {
            // ����ƽ��֡��
            _fps = _accumulator / _frameCount;
            _timeLeft = updateInterval;
            _accumulator = 0f;
            _frameCount = 0;

            // ������ʾ�ı�������һλС��
            fpsText.text = $"FPS: {_fps:0.0}";

            // ����֡�����ò�ͬ��ɫ��ֱ����ʾ����
            if (_fps < 30)
                fpsText.color = Color.yellow;
            else if (_fps < 15)
                fpsText.color = Color.red;
            else
                fpsText.color = Color.green;
        }
    }
}
