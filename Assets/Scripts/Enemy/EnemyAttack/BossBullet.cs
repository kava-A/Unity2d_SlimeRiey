using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class BossBullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 20;
    public GameObject smallBulletPrefab;
    public GameObject[] initPoint;
    private float rotateSpeed = 180f; // ��ת��ת�ٶ�
    private Vector2 shootDirection;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        StartCoroutine(SetVelocityAfterFrame());
        Debug.Log("���䷽��" + shootDirection + "���ٶȣ�" + (shootDirection * speed));
        rb.velocity = shootDirection * speed;
    }
    IEnumerator SetVelocityAfterFrame()
    {
        yield return null; // ��һ֡��ȷ��SetShootDirection��ִ��
        rb.velocity = shootDirection * speed;
        Debug.Log("�ӳٺ����õ��ٶȣ�" + rb.velocity);
    }
    /// <summary>
    /// �����ӵ��������
    /// </summary>
    /// <param name="direction"></param>
    public void SetShootDirection(Vector2 direction)
    {
        shootDirection = direction;
        Debug.Log(direction);
    }


    private void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStatus>().TakeDamage(damage);
            Boom();
            gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Enemy"))
        {
            
        }
        else
        {
            Boom();
            gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// ���ٺ󴴽�С�ӵ�
    /// </summary>
    private void Boom()
    {
        for (int i = 0; i < initPoint.Length; i++)
        {
            Instantiate(smallBulletPrefab, initPoint[i].transform.position, initPoint[i].transform.rotation);
        }
    }
}
