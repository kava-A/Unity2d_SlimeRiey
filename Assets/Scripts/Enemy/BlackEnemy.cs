using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEnemy : Enemy
{
    private float lr;//用于计算与玩家的横向坐标，判断处于玩家的左右
    private void Start()
    {
        StartCoroutine(EnemyMove());
    }
    IEnumerator EnemyMove()
    {
        //持续移动，每隔1秒跳跃一次
        while (true)
        {
            Vector2 dir = (player.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);


            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            yield return new WaitForSeconds(1f);
        }
    }
    private void Update()
    {
        lr = transform.position.x - player.transform.position.x;//计算玩家与自身的横向坐标
        //通过横向坐标判断朝向
        if (lr < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    /// <summary>
    /// 敌人死亡
    /// 停止协程
    /// </summary>
    protected override void Dead()
    {
        base.Dead();
        StopCoroutine(EnemyMove());
        gameObject.SetActive(false);
        
    }
}
