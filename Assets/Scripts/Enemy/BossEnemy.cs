using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{

    private float lr;//用于计算与玩家的横向坐标，判断处于玩家的左右
    private bool canMove;
    private void Start()
    {
        canMove = true;
        StartCoroutine(EnemyMove());
    }
    IEnumerator EnemyMove()
    {
        if (canMove)
        {

            //持续移动，每隔1秒跳跃一次
            while (true)
            {
                Vector2 dir = (player.transform.position - transform.position).normalized;
                rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);


                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                yield return new WaitForSeconds(2f);
            }
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
    public override void Freeze()
    {
        canMove=false;
        EnemyAtk();
    }
    public void EnemyAtk()
    {
        
        
        canMove=true;
    }
    /// <summary>
    /// 敌人死亡
    /// 停止协程
    /// </summary>
    protected override void Dead()
    {

        StopCoroutine(EnemyMove());
        gameObject.SetActive(false);
        canMove=false;

    }
}
