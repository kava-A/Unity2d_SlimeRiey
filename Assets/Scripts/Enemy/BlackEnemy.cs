using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// С�͵����Դ�Ѳ�ߣ�ֻҪ��Ҳ���������˺������ͻ�һֱѲ��
/// </summary>
public class BlackEnemy : Enemy
{
    [Header("С�͵����������")]
    [Tooltip("Ѳ�߷�Χ")] public float patrolRange;
    [Tooltip("����ص��ȴ�ʱ��")] public float waitTime;
    [Tooltip("Ŀ��ص�")] private Vector2 targetPosition;
    private float waitTimer;
    private bool isGround;
    private float lr;//���ڼ�������ҵĺ������꣬�жϴ�����ҵ�����
    private bool getHert = false;//�Ƿ�����
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

            lr = transform.position.x - player.transform.position.x;//�������������ĺ�������
        }
        //ͨ�����������жϳ���
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
            rb.velocity = new Vector2(0, rb.velocity.y);//����������ٶ�
            // ����Ŀ����ȴ�
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                SetRandomTarget();
                waitTimer = 0;
            }
        }
        else
        {
            // �ƶ������Ŀ���
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
        //�����ƶ���ÿ��1����Ծһ��
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
    /// ��������
    /// ֹͣЭ��
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
