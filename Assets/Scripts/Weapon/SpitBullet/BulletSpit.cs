using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 口水子弹
/// </summary>
public class BulletSpit : Weapon
{
    [SerializeField, Tooltip("子弹粒子特效，属于子物体，且未启用状态")] public GameObject[] effect;
    [SerializeField, Tooltip("爆炸特效预制体")] public GameObject explosion;

    private int index = 0;
    GameObject go;//用于临时存储爆炸特效
    private void Start()
    {
        rb.velocity = transform.right * speed;
        //Destroy(gameObject, range);
    }
    private void OnEnable()
    {
        index = Random.Range(0, effect.Length);//随机获取一个子弹粒子特效
        effect[index].SetActive(true);

        go = Instantiate(explosion);//创建爆炸特效
        go.SetActive(false);//设置爆炸特效为未激活
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            effect[index].SetActive(false);

            Destroy(gameObject);
        }
        else if (!collision.CompareTag("Player"))
        {
            Destroy(gameObject);

        }
    }
    /// <summary>
    /// 当游戏物体销毁时启用爆炸特效
    /// </summary>
    private void OnDestroy()
    {
        go.transform.position = transform.position;//设置爆炸特效坐标
        go.transform.rotation = Quaternion.identity;//不旋转
        go.SetActive(true);
    }
}
