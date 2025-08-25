using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("��������")]
    [SerializeField, Tooltip("����Ѫ��")] protected float health;
    [SerializeField, Tooltip("�����ƶ��ٶ�")] protected float moveSpeed;
<<<<<<< HEAD
    [SerializeField, Tooltip("������Ծ��")] protected float jumpForce;
    [SerializeField, Tooltip("������ײ�˺�")] protected float collisionDamage;

    [Header("���˽���")]
    [SerializeField, Tooltip("���˱���ɱ�������ͳ��")] protected int killCount;
=======
    [SerializeField, Tooltip("�����ƶ��ٶ�")] protected float jumpForce;
    [SerializeField, Tooltip("������ײ�˺�")] protected float collisionDamage;

    [Header("���˽���")]
    [SerializeField,Tooltip("���˱���ɱ�������ͳ��")] protected int killCount;
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    [SerializeField, Tooltip("���˱���ɱ���õĽ��")] protected int enemyPrice;


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
        GameManager.Instance.AddKillCount(killCount, enemyPrice);
        //gameObject.SetActive(false);
<<<<<<< HEAD
       
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStatus>().TakeDamage(collisionDamage);
        }
    }
<<<<<<< HEAD
    public virtual void TakeDamage(float amount)
    {
        if (health <= 0)
        {
            Dead();
        }
        health = Mathf.Max(health -= amount, 0);
        //Debug.Log(health.ToString("F2"));
        //Debug.Log(amount.ToString("F2"));
=======
    public void TakeDamage(float amount)
    {
        if (health > 0)
        {
            health = Mathf.Max(health -= amount, 0);
            //Debug.Log(health.ToString("F2"));
            //Debug.Log(amount.ToString("F2"));
            if (health <= 0)
            {
                Dead();
            }
        }
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    }
}
