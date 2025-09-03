using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 小型敌人自带巡逻，只要玩家不对它造成伤害，它就会一直巡逻
/// </summary>
public class BlackEnemy : Enemy
{
    [Header("小型敌人属性相关")]
    [Tooltip("巡逻范围")] public float patrolRange;
    [Tooltip("到达地点后等待时间")] public float waitTime;
    [Tooltip("目标地点")] private Vector2 targetPosition;
    private float waitTimer;
    private bool isGround;
    private float lr;//用于计算与玩家的横向坐标，判断处于玩家的左右
    private bool getHert = false;//是否受伤
    private void Start()
    {
        getHert = false;
        SetRandomTarget();
    }

    private void Update()
    {
        if (getHert == false)
        {
            lr = transform.position.x - targetPosition.x;
        }
        else
        {

            lr = transform.position.x - player.transform.position.x;//计算玩家与自身的横向坐标
        }
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
    private void FixedUpdate()
    {
        if (getHert == true)
        {
            return;
        }
        Patrol();


    }
    private void Patrol()
    {
        if (Mathf.Abs(transform.position.x - targetPosition.x) <= 0.5f)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);//到达后清零速度
            // 到达目标点后等待
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                SetRandomTarget();
                waitTimer = 0;
            }
        }
        else
        {
            // 移动到随机目标点
            Vector2 dir = (targetPosition - (Vector2)transform.position).normalized;
            rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);

        }
    }
    private void SetRandomTarget()
    {
        if (getHert == true)
        {
            return;
        }
        float randomX = Random.Range(-patrolRange, patrolRange) + transform.position.x;
        //float randomY = Random.Range(-patrolRange, patrolRange) + transform.position.y;
        targetPosition = new Vector2(randomX, transform.position.y);
    }

    IEnumerator EnemyMove()
    {
        //持续移动，每隔1秒跳跃一次
        while (true)
        {
            Vector2 dir = (player.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);

            if (isGround)
            {

                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);

        getHert = true;

        StartCoroutine(EnemyMove());
    }
    /// <summary>
    /// 敌人死亡
    /// 停止协程
    /// </summary>
    protected override void Dead()
    {
        base.Dead();

        getHert = false;

        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        StopCoroutine(EnemyMove());
    }
}
