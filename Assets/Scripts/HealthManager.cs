using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour,IDamageable
{
    public static HealthManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public Slider hpSlider;
    public float currenthealth;
    public float maxHealth;
    public float healthRegen;
    private void Start()
    {
        currenthealth=maxHealth;
    }

    private void Update()
    {
        hpSlider.value = GetParcentage();
        Healing(healthRegen*Time.deltaTime);//每秒回血
    }

    public void Heal(float amount)
    {
        if (amount < 0) { Debug.Log("回血值异常"); return; }
        currenthealth = Mathf.Min(currenthealth + amount, maxHealth);
    }

    /// <summary>
    /// 回血
    /// </summary>
    /// <param name="amount">值</param>
    public void Healing(float amount)
    {
        currenthealth = Mathf.Min(currenthealth + amount, maxHealth);//玩家血量不会超过最大血量
    }

    public void TakeDamage(float amout)
    {
        currenthealth = Mathf.Max(currenthealth - amout, 0);//玩家血量将不会低于0

        if (currenthealth == 0)
        {
            gameObject.SetActive(false);
        }
    }

    public float GetParcentage()
    {
        return currenthealth / maxHealth;
    }
}




