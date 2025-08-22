using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField, Tooltip("Enemy mouth")] private Transform mouth;
    [SerializeField, Tooltip("BulletPrefab")] private GameObject bulletPrefab;

    public int bulletCount;//预先创造子弹数量

    private float lr;//用于计算与玩家的横向坐标，判断处于玩家的左右
    private bool isDead;
    private Coroutine moveCoroutine;//保存移动协程的引用
    private void Start()
    {

        PoolManager.Instance.InitPool(bulletPrefab, bulletCount);

        moveCoroutine= StartCoroutine(EnemyMove());
        isDead =false;
    }
    IEnumerator EnemyMove()
    { 
        ////持续移动，每隔1秒跳跃一次
        //while (true)
        //{
        //    Vector2 dir = (player.transform.position - transform.position).normalized;
        //    rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);

        //    yield return new WaitForSeconds(1f);
        //    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        //    yield return new WaitForSeconds(2f);
        //}
        while (!isDead)
        {
            // 攻击逻辑
            GameObject bullet = PoolManager.Instance.GetObject(bulletPrefab);
            bullet.transform.position = mouth.position;
            bullet.transform.rotation = mouth.rotation;
            bullet.GetComponent<BossBullet>().SetShootDirection(mouth.right);

            // 攻击后等待1秒，然后开始移动逻辑
            yield return new WaitForSeconds(1f);

            // 移动逻辑 - 执行一次移动循环（移动、跳跃、等待）
            Vector2 dir = (player.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);

            yield return new WaitForSeconds(1f); // 移动1秒后跳跃
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            yield return new WaitForSeconds(2f); // 跳跃后等待2秒，然后回到攻击逻辑
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

        Vector2 target = player.transform.position;
        Vector2 direction = target - (Vector2)mouth.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        mouth.rotation = Quaternion.Euler(new Vector3(0, 0, angle));//使嘴巴朝向玩家

    }
    //IEnumerator EnemyAttack()
    //{
    //    while (!isDead)
    //    {
    //        GameObject bullet = PoolManager.Instance.GetObject(bulletPrefab);
    //        bullet.transform.position = mouth.position;
    //        bullet.transform.rotation = mouth.rotation;
    //        bullet.GetComponent<BossBullet>().SetShootDirection(mouth.right);//在创建对象池的时候已经设置了方向，所以无论需要在子弹使用的时候单独再设置一次方向

    //        yield return new WaitForSeconds(1f);
    //        StartCoroutine(EnemyMove());

    //    }

    //}
    public override void Freeze()
    {
        Debug.Log($"{gameObject.name} is frozen");
        // 停止移动协程
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
        Invoke(nameof(EnemyAtk), 3f);//2秒后恢复攻击
        
    }
    public void EnemyAtk()
    {
        if(isDead) return;
        // 重新启动移动协程
        if (moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(EnemyMove());
        }
    }
    /// <summary>
    /// 敌人死亡
    /// 停止协程
    /// </summary>
    protected override void Dead()
    {
        base.Dead();
        isDead = true;
        StopCoroutine(EnemyMove());
        gameObject.SetActive(false);
    }
}
