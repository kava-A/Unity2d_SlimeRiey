using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerStatus : MonoBehaviourPun, IDamageable
{
    [SerializeField, Tooltip("血条UI")] private Slider hpSlider;

    [Header("玩家生命值")]
    [Tooltip("当前血量")] public float currenthealth;
    [Tooltip("最大血量")] public float maxHealth;
    [Tooltip("每秒回血值")] public float healthRegen;


    private int allStatusM = 1;//状态总加成倍率
    public int AllStatusM { get => allStatusM; set => allStatusM = value; }

    private TextMeshProUGUI hpText;
    private void OnEnable()
    {
        if (!photonView.IsMine)
        {
            return;
        }//不是本地玩家且联网状态下不执行后续代码
        FindHPSlider();
    }
    // 查找血条
    private void FindHPSlider()
    {
        hpSlider = GameObject.Find("Canvas/SafeArea/BaseInfoPanel/HPSlider")?.GetComponent<Slider>();
        if (hpSlider != null)
        {
            // 找到血条后立即获取文本组件
            hpText = hpSlider.GetComponentInChildren<TextMeshProUGUI>();
            if (hpText == null)
            {
                Debug.LogError("血条文本组件未找到，请检查Slider子物体");
            }
            else
            {
                // 初始化血条显示（防止延迟导致的初始值错误）
                GetParcentage();
            }
            CancelInvoke(nameof(FindHPSlider)); // 找到后停止重试
        }
        else
        {
            Debug.LogError("血条Slider未找到，路径：Canvas/SafeArea/BaseInfoPanel/HPSlider");
            Invoke(nameof(FindHPSlider), 0.5f); // 继续重试
        }
    }
    private void Start()
    { 
        // 非本地玩家直接返回
        if (!photonView.IsMine)
        {
            return;
        }
        
        
        currenthealth = maxHealth;
        GetParcentage();
    }
    public void ChangeMaxHealth(int value)
    {

        maxHealth = maxHealth / value;
        currenthealth = currenthealth / value;
        GetParcentage();
    }
    private void Update()
    {
        Healing(healthRegen * allStatusM * Time.deltaTime);
    }
    /// <summary>
    /// 自愈
    /// </summary>
    /// <param name="amount">回血值</param>
    public void Healing(float amount)
    {
        currenthealth = Mathf.Min(currenthealth + amount, maxHealth);//玩家血量不会超过最大血量
        GetParcentage();
    }
    /// <summary>
    /// 治疗
    /// </summary>
    /// <param name="amount">回血值</param>
    public void Heal(float amount)
    {
        if (amount <= 0) { Debug.Log("回血值异常"); return; }
        currenthealth = Mathf.Min(currenthealth + amount, maxHealth);
        Debug.Log($"治疗前血量: {currenthealth}, 治疗量: {amount}, 最大血量: {maxHealth}");
        GetParcentage();
    }

    /// <summary>
    /// 更新血条
    /// </summary>
    public void GetParcentage()
    {
        if (!photonView.IsMine || hpSlider == null || hpText == null)
        {
            return;
        }
        hpSlider.value = currenthealth / maxHealth;
        hpText.text = currenthealth.ToString("F1");
    }

    /// <summary>
    /// 玩家受伤
    /// </summary>
    /// <param name="amount">伤害值</param>
    public void TakeDamage(float amount)
    {
        currenthealth = Mathf.Max(currenthealth - amount * allStatusM, 0);//玩家血量将不会低于0

        GetParcentage();
        if (currenthealth <= 0)
        {
            GameManager.Instance.DeLifeCount(1);
            currenthealth = maxHealth;
        }
    }

}
