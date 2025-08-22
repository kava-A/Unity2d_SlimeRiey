using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField, Tooltip("Enemy mouth")] private Transform mouth;
    [SerializeField, Tooltip("BulletPrefab")] private GameObject bulletPrefab;

    public int bulletCount;//Ԥ�ȴ����ӵ�����

    private float lr;//���ڼ�������ҵĺ������꣬�жϴ�����ҵ�����
    private bool isDead;
    private Coroutine moveCoroutine;//�����ƶ�Э�̵�����
    private void Start()
    {

        PoolManager.Instance.InitPool(bulletPrefab, bulletCount);

        moveCoroutine= StartCoroutine(EnemyMove());
        isDead =false;
    }
    IEnumerator EnemyMove()
    { 
        ////�����ƶ���ÿ��1����Ծһ��
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
            // �����߼�
            GameObject bullet = PoolManager.Instance.GetObject(bulletPrefab);
            bullet.transform.position = mouth.position;
            bullet.transform.rotation = mouth.rotation;
            bullet.GetComponent<BossBullet>().SetShootDirection(mouth.right);

            // ������ȴ�1�룬Ȼ��ʼ�ƶ��߼�
            yield return new WaitForSeconds(1f);

            // �ƶ��߼� - ִ��һ���ƶ�ѭ�����ƶ�����Ծ���ȴ���
            Vector2 dir = (player.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);

            yield return new WaitForSeconds(1f); // �ƶ�1�����Ծ
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            yield return new WaitForSeconds(2f); // ��Ծ��ȴ�2�룬Ȼ��ص������߼�
        }

    }
    private void Update()
    {
        lr = transform.position.x - player.transform.position.x;//�������������ĺ�������
        //ͨ�����������жϳ���
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
        mouth.rotation = Quaternion.Euler(new Vector3(0, 0, angle));//ʹ��ͳ������

    }
    //IEnumerator EnemyAttack()
    //{
    //    while (!isDead)
    //    {
    //        GameObject bullet = PoolManager.Instance.GetObject(bulletPrefab);
    //        bullet.transform.position = mouth.position;
    //        bullet.transform.rotation = mouth.rotation;
    //        bullet.GetComponent<BossBullet>().SetShootDirection(mouth.right);//�ڴ�������ص�ʱ���Ѿ������˷�������������Ҫ���ӵ�ʹ�õ�ʱ�򵥶�������һ�η���

    //        yield return new WaitForSeconds(1f);
    //        StartCoroutine(EnemyMove());

    //    }

    //}
    public override void Freeze()
    {
        Debug.Log($"{gameObject.name} is frozen");
        // ֹͣ�ƶ�Э��
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
        Invoke(nameof(EnemyAtk), 3f);//2���ָ�����
        
    }
    public void EnemyAtk()
    {
        if(isDead) return;
        // ���������ƶ�Э��
        if (moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(EnemyMove());
        }
    }
    /// <summary>
    /// ��������
    /// ֹͣЭ��
    /// </summary>
    protected override void Dead()
    {
        base.Dead();
        isDead = true;
        StopCoroutine(EnemyMove());
        gameObject.SetActive(false);
    }
}
