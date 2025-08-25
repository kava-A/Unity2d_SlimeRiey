using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 口水子弹
<<<<<<< HEAD
/// 不会与玩家、NPC、武器进行碰撞检测
/// 撞击到敌人和其他物品会销毁
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
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
<<<<<<< HEAD
        else if (!collision.CompareTag("Player")&&!collision.CompareTag("NPC")&&!collision.CompareTag("Weapon"))
=======
        else if (!collision.CompareTag("Player"))
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
        {
            Destroy(gameObject);

        }
    }
<<<<<<< HEAD
    private void OnDisable()
    {
        effect[index].SetActive(false);
        go.transform.position = transform.position;//设置爆炸特效坐标
        go.transform.rotation = Quaternion.identity;//不旋转
        go.SetActive(true);
    }
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    /// <summary>
    /// 当游戏物体销毁时启用爆炸特效
    /// </summary>
    private void OnDestroy()
    {
<<<<<<< HEAD
        
=======
        go.transform.position = transform.position;//设置爆炸特效坐标
        go.transform.rotation = Quaternion.identity;//不旋转
        go.SetActive(true);
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
    }
}
