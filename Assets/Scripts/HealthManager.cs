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
        Healing(healthRegen*Time.deltaTime);//ÿ���Ѫ
    }

    public void Heal(float amount)
    {
        if (amount < 0) { Debug.Log("��Ѫֵ�쳣"); return; }
        currenthealth = Mathf.Min(currenthealth + amount, maxHealth);
    }

    /// <summary>
    /// ��Ѫ
    /// </summary>
    /// <param name="amount">ֵ</param>
    public void Healing(float amount)
    {
        currenthealth = Mathf.Min(currenthealth + amount, maxHealth);//���Ѫ�����ᳬ�����Ѫ��
    }

    public void TakeDamage(float amout)
    {
        currenthealth = Mathf.Max(currenthealth - amout, 0);//���Ѫ�����������0

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




