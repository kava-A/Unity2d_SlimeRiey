using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    [SerializeField, Tooltip("血条UI")] private Slider hpSlider;

    [Header("玩家生命值")]
    [HideInInspector,Tooltip("当前血量")] public float currenthealth;
    [Tooltip("最大血量")] public float maxHealth;
    [Tooltip("每秒回血值")] public float healthRegen;

    private void Start()
    {
        currenthealth = maxHealth;
        GetParcentage();
    }
    private void Update()
    {
        Healing(healthRegen * Time.deltaTime);
    }
    /// <summary>
    /// 自愈
    /// </summary>
    /// <param name="amount">回血值</param>
    public void Healing(float amount)
    {
        currenthealth = Mathf.Min(currenthealth + amount, maxHealth);//玩家血量不会超过最大血量
    }
    /// <summary>
    /// 治疗
    /// </summary>
    /// <param name="amount">回血值</param>
    public void Heal(float amount)
    {
        if (amount <= 0) { Debug.Log("回血值异常"); return; }
        currenthealth = Mathf.Min(currenthealth + amount, maxHealth);
    }

    /// <summary>
    /// 更新血条
    /// </summary>
    public void GetParcentage()
    {
        hpSlider.value = currenthealth / maxHealth;
    }

    /// <summary>
    /// 玩家受伤
    /// </summary>
    /// <param name="amount">伤害值</param>
    public void TakeDamage(float amount)
    {
        currenthealth = Mathf.Max(currenthealth - amount, 0);//玩家血量将不会低于0

        GetParcentage();
    }
}
