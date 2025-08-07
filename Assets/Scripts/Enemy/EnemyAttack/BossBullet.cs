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
    private float rotateSpeed = 180f; // 自转旋转速度
    private Vector2 shootDirection;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        StartCoroutine(SetVelocityAfterFrame());
        Debug.Log("发射方向：" + shootDirection + "，速度：" + (shootDirection * speed));
        rb.velocity = shootDirection * speed;
    }
    IEnumerator SetVelocityAfterFrame()
    {
        yield return null; // 等一帧，确保SetShootDirection已执行
        rb.velocity = shootDirection * speed;
        Debug.Log("延迟后设置的速度：" + rb.velocity);
    }
    /// <summary>
    /// 设置子弹射击方向
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
    /// 销毁后创建小子弹
    /// </summary>
    private void Boom()
    {
        for (int i = 0; i < initPoint.Length; i++)
        {
            Instantiate(smallBulletPrefab, initPoint[i].transform.position, initPoint[i].transform.rotation);
        }
    }
}
