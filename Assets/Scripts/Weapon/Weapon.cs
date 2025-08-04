using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField, Tooltip("武器名称")] protected string weaponName;
    [SerializeField, Tooltip("武器伤害")] protected float damage;
    [SerializeField, Tooltip("武器射程")] protected float range;
    [SerializeField, Tooltip("武器飞行速度")] protected float speed;
    [SerializeField, Tooltip("攻击冷却时间")] protected float attackCooldown;
    [SerializeField, Tooltip("武器类型")] protected WeaponType weaponType;
    [SerializeField, Tooltip("上次攻击时间")] protected float lastAttackTime;
    [SerializeField, Tooltip("是否可以攻击")] protected bool CanAttack => Time.time >= lastAttackTime + attackCooldown;
    protected Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void Attack()
    {
    }

    /// <summary>
    /// 装备武器
    /// </summary>
    public virtual void Equip()
    {
        gameObject.SetActive(true);
        Debug.Log($"{weaponName} 已装备");
    }

    /// <summary>
    /// 卸下武器
    /// </summary>
    public virtual void Unequip()
    {
        gameObject.SetActive(false);
        Debug.Log($"{weaponName} 已卸下");
    }

    // 显示武器信息
    public virtual string GetWeaponInfo()
    {
        return $"{weaponName} - 伤害: {damage}, 攻击间隔: {attackCooldown}秒";
    }

}

/// <summary>
/// 武器类型
/// 近战
/// 远程
/// 投掷
/// </summary>
public enum WeaponType
{
    Melee,
    Ranged,
    Throwable
}
