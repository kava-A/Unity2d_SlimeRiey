using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class UnicornEnemy : Enemy
{
    private bool atked = false;
    private SpriteRenderer sr;
    private float lr;
    private Vector2 currentDir; // 当前移动方向
    private void OnEnable()
    {
        sr = GetComponent<SpriteRenderer>();
        atked = false;
        rb.gravityScale = 1;
    }
    private void OnDisable()
    {
        atked = false;
        rb.gravityScale = 1;
    }
    private void Update()
    {
        if (player == null)
        {

            FindLocalPlayer();
            
        }
        lr = transform.position.x - player.transform.position.x;

        sr.flipX = lr >= 0;

        currentDir = (player.transform.position - transform.position).normalized;
        

        if (!atked)
        {

            
            rb.velocity = new Vector2(currentDir.x * moveSpeed, rb.velocity.y);
            atked = true;
        }
        else
        {

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dead();
        }
        else
        {
            rb.gravityScale = 8;
            if (IsSideCollision(collision))
            {
                JumpOverSetp();
            }
            else
            {
                rb.velocity = new Vector2(currentDir.x, jumpForce );
                atked = false;
            }

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        atked = false;
    }
    private void JumpOverSetp()
    {
        rb.velocity = new Vector2(currentDir.x, jumpForce);
        StartCoroutine(DisableMove(0.25f));
    }
    private bool IsSideCollision(Collision2D collision)
    {
        // 获取碰撞点的法线方向（与碰撞面垂直）
        ContactPoint2D contact = collision.contacts[0];
        // 法线接近垂直（角度小于30度），说明是侧面碰撞（台阶）
        return Mathf.Abs(contact.normal.y) < 0.5f; // 0.5对应60度，值越小判断越严格
    }
    IEnumerator DisableMove(float time)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        atked = true;
        yield return new WaitForSeconds(time);
        atked = false;
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        rb.velocity = Vector2.zero;

    }
    protected override void Dead()
    {
        base.Dead();
        PoolManager.Instance.ReturnObject(gameObject, gameObject);
    }
}
