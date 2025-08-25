using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour, IDamageable
{
    [SerializeField, Tooltip("Ѫ��UI")] private Slider hpSlider;

    [Header("�������ֵ")]
    [Tooltip("��ǰѪ��")] public float currenthealth;
    [Tooltip("���Ѫ��")] public float maxHealth;
    [Tooltip("ÿ���Ѫֵ")] public float healthRegen;


    private int allStatusM=1;//״̬�ܼӳɱ���
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
    }
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="amount">��Ѫֵ</param>
    public void Healing(float amount)
    {
        currenthealth = Mathf.Min(currenthealth + amount, maxHealth);//���Ѫ�����ᳬ�����Ѫ��
        GetParcentage();
    }
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="amount">��Ѫֵ</param>
    public void Heal(float amount)
    {
        if (amount <= 0) { Debug.Log("��Ѫֵ�쳣"); return; }
        currenthealth = Mathf.Min(currenthealth + amount, maxHealth);
        Debug.Log($"����ǰѪ��: {currenthealth}, ������: {amount}, ���Ѫ��: {maxHealth}");
        GetParcentage();
    }

    /// <summary>
    /// ����Ѫ��
    /// </summary>
    public void GetParcentage()
    {
        hpSlider.value = currenthealth / maxHealth;
        hpText.text = currenthealth.ToString("F1");
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="amount">�˺�ֵ</param>
    public void TakeDamage(float amount)
    {
        currenthealth = Mathf.Max(currenthealth - amount*allStatusM, 0);//���Ѫ�����������0

        GetParcentage();
    }
}
