using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField, Tooltip("��������")] protected string weaponName;
    [SerializeField, Tooltip("�����˺�")] protected float damage;
    [SerializeField, Tooltip("�������")] protected float range;
    [SerializeField, Tooltip("���������ٶ�")] protected float speed;
    [SerializeField, Tooltip("������ȴʱ��")] protected float attackCooldown;
    [SerializeField, Tooltip("��������")] protected WeaponType weaponType;
    [SerializeField, Tooltip("�ϴι���ʱ��")] protected float lastAttackTime;
    [SerializeField, Tooltip("�Ƿ���Թ���")] protected bool CanAttack => Time.time >= lastAttackTime + attackCooldown;
    protected Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void Attack()
    {
    }

    /// <summary>
    /// װ������
    /// </summary>
    public virtual void Equip()
    {
        gameObject.SetActive(true);
        Debug.Log($"{weaponName} ��װ��");
    }

    /// <summary>
    /// ж������
    /// </summary>
    public virtual void Unequip()
    {
        gameObject.SetActive(false);
        Debug.Log($"{weaponName} ��ж��");
    }

    // ��ʾ������Ϣ
    public virtual string GetWeaponInfo()
    {
        return $"{weaponName} - �˺�: {damage}, �������: {attackCooldown}��";
    }

}

/// <summary>
/// ��������
/// ��ս
/// Զ��
/// Ͷ��
/// </summary>
public enum WeaponType
{
    Melee,
    Ranged,
    Throwable
}
