using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("敌人属性")]
    [SerializeField, Tooltip("敌人血量")] protected float health;
    [SerializeField, Tooltip("敌人移动速度")] protected float moveSpeed;
    [SerializeField, Tooltip("敌人移动速度")] protected float jumpForce;
    [SerializeField, Tooltip("敌人碰撞伤害")] protected float collisionDamage;

    [SerializeField] protected GameObject player;
    protected Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void Freeze()
    {

    }
    protected virtual void Dead()
    {

        //gameObject.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStatus>().TakeDamage(collisionDamage);
        }
    }
    public void TakeDamage(float amount)
    {
        if (health > 0)
        {
            health = Mathf.Max(health -= amount, 0);
            Debug.Log(health.ToString("F2"));
            Debug.Log(amount.ToString("F2"));
            if (health <= 0)
            {
                Dead();
            }
        }
    }
}
