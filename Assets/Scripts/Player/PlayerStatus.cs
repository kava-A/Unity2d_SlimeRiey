using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
using TMPro;
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    [SerializeField, Tooltip("血条UI")] private Slider hpSlider;

    [Header("玩家生命值")]
    [Tooltip("当前血量")] public float currenthealth;
    [Tooltip("最大血量")] public float maxHealth;
    [Tooltip("每秒回血值")] public float healthRegen;

<<<<<<< HEAD

    private int allStatusM=1;//状态总加成倍率
    public int AllStatusM { get => allStatusM; set => allStatusM = value; }

    private TextMeshProUGUI hpText;
    private void Start()
    {
        hpText = hpSlider.GetComponentInChildren<TextMeshProUGUI>();
        currenthealth = maxHealth;
        GetParcentage();
    }
    public void ChangeMaxHealth(int value)
    {
        
        maxHealth = maxHealth/value;
        currenthealth = currenthealth/value;
        GetParcentage();
    }
    private void Update()
    {
        Healing(healthRegen*allStatusM * Time.deltaTime);
=======
    private void Start()
    {
        currenthealth = maxHealth;
        GetParcentage();
    }
    private void Update()
    {
        Healing(healthRegen * Time.deltaTime);
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
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
        hpSlider.value = currenthealth / maxHealth;
<<<<<<< HEAD
        hpText.text = currenthealth.ToString("F1");
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    }

    /// <summary>
    /// 玩家受伤
    /// </summary>
    /// <param name="amount">伤害值</param>
    public void TakeDamage(float amount)
    {
<<<<<<< HEAD
        currenthealth = Mathf.Max(currenthealth - amount*allStatusM, 0);//玩家血量将不会低于0
=======
        currenthealth = Mathf.Max(currenthealth - amount, 0);//玩家血量将不会低于0
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892

        GetParcentage();
    }
}
