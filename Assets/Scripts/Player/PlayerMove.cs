using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;


    [Header("移动")]
    [SerializeField, Tooltip("移动速度")] public float moveSpeed;
    [SerializeField, Tooltip("跳跃力")] private float jumpForce;


    [Tooltip("碰撞接触统计，用于跳跃判定")] private int collisionCount = 0;
    private Animator animator;
    private float inputX;
    private float lastInputX;//记录上次横向输入值
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        lastInputX = inputX;
        Walk();

        //跳跃
        if (Input.GetKeyDown(KeyCode.Space) && collisionCount > 0)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }


    }
    /// <summary>
    /// 玩家移动
    /// </summary>
    private void Walk()
    {
        //通过上次横向输入值判断面朝哪个方向
        if (lastInputX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (lastInputX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

    }

    /// <summary>
    /// 碰撞统计，如果玩家碰撞到了
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == 6)
        //{

        //}
            collisionCount++;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
            collisionCount--;

    }

}
