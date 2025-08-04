using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    [SerializeField, Tooltip("Ѫ��UI")] private Slider hpSlider;

    [Header("�������ֵ")]
    [HideInInspector,Tooltip("��ǰѪ��")] public float currenthealth;
    [Tooltip("���Ѫ��")] public float maxHealth;
    [Tooltip("ÿ���Ѫֵ")] public float healthRegen;

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
    /// ����
    /// </summary>
    /// <param name="amount">��Ѫֵ</param>
    public void Healing(float amount)
    {
        currenthealth = Mathf.Min(currenthealth + amount, maxHealth);//���Ѫ�����ᳬ�����Ѫ��
    }
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="amount">��Ѫֵ</param>
    public void Heal(float amount)
    {
        if (amount <= 0) { Debug.Log("��Ѫֵ�쳣"); return; }
        currenthealth = Mathf.Min(currenthealth + amount, maxHealth);
    }

    /// <summary>
    /// ����Ѫ��
    /// </summary>
    public void GetParcentage()
    {
        hpSlider.value = currenthealth / maxHealth;
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="amount">�˺�ֵ</param>
    public void TakeDamage(float amount)
    {
        currenthealth = Mathf.Max(currenthealth - amount, 0);//���Ѫ�����������0

        GetParcentage();
    }
}
