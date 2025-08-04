using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{

    private float lr;//���ڼ�������ҵĺ������꣬�жϴ�����ҵ�����
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

            //�����ƶ���ÿ��1����Ծһ��
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
    /// ��������
    /// ֹͣЭ��
    /// </summary>
    protected override void Dead()
    {

        StopCoroutine(EnemyMove());
        gameObject.SetActive(false);
        canMove=false;

    }
}
