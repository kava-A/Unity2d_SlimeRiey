using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("敌人属性")]
    [SerializeField, Tooltip("敌人血量")] protected float health;
    [SerializeField, Tooltip("敌人移动速度")] protected float moveSpeed;
    [SerializeField, Tooltip("敌人跳跃力")] protected float jumpForce;
    [SerializeField, Tooltip("敌人碰撞伤害")] protected float collisionDamage;

    [Header("敌人奖励")]
    [SerializeField, Tooltip("敌人被击杀后的数量统计")] protected int killCount;
    [SerializeField, Tooltip("敌人被击杀后获得的金币")] protected int enemyPrice;


    [SerializeField] protected GameObject player;
    protected Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        EventDefine.OnPlayerSpawned += OnPlayerFound;
    }
    private void OnDestroy()
    {
        EventDefine.OnPlayerSpawned -= OnPlayerFound;
    }
    private void OnPlayerFound(GameObject playerObj)
    {
        PhotonView pv = playerObj.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine)
        {
            player = playerObj;
            Debug.Log("通过事件更新玩家引用");
        }
    }
    /// <summary>
    /// 主动寻找本地玩家
    /// </summary>
    protected void FindLocalPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var p in players)
        {
            PhotonView pv = p.GetComponent<PhotonView>();
            if (pv != null && pv.IsMine) // 只认本地玩家
            {
                player = p;
                Debug.Log($"{gameObject.name} 主动找到本地玩家");
                return;
            }
        }
        Debug.LogWarning($"{gameObject.name} 主动查找玩家失败，1秒后重试");
        Invoke(nameof(FindLocalPlayer), 1f); // 重试机制
    }
    public virtual void Freeze()
    {

    }
    protected virtual void Dead()
    {
        GameManager.Instance.AddKillCount(killCount, enemyPrice);
        //gameObject.SetActive(false);
       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStatus>().TakeDamage(collisionDamage);
        }
    }
    public virtual void TakeDamage(float amount)
    {
        if (health <= 0)
        {
            Dead();
        }
        health = Mathf.Max(health -= amount, 0);
        //Debug.Log(health.ToString("F2"));
        //Debug.Log(amount.ToString("F2"));
    }
}
