using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;


    [Header("移动")]
    [SerializeField, Tooltip("移动速度")] public float moveSpeed;
    [SerializeField, Tooltip("跳跃力")] private float jumpForce;//正常跳跃力
    [SerializeField,Tooltip("加速倍率")]private float accelerate = 1.1f;
    [Header("多段跳设置")]
    public int jumpMaxCount = 2;
    public float exForce=70;//二段跳


    [Tooltip("碰撞接触统计，用于跳跃判定")] private int collisionCount = 0;
    private Animator animator;
    private float inputX;
    private float lastInputX;//记录上次横向输入值
    private int jumpCount = 0;
    private float trueMoveSpeed;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = jumpMaxCount;
        trueMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        accelerate=Mathf.Clamp(accelerate,1,2);//限制加速度在1-2倍之间
        inputX = Input.GetAxis("Horizontal");
        lastInputX = inputX;
        Walk();

        //跳跃
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (collisionCount > 0)
            {
                animator.SetTrigger("Jump");
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount -= 1;

            }
            else if (jumpCount != 0)
            {
                animator.SetTrigger("Jump");
                rb.velocity = new Vector2(rb.velocity.x, exForce);
                jumpCount -= 1;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            trueMoveSpeed = moveSpeed*accelerate;
        }
        else
        {
            trueMoveSpeed = moveSpeed;
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
        rb.velocity = new Vector2(inputX * trueMoveSpeed, rb.velocity.y);

    }

    /// <summary>
    /// 碰撞统计，用于跳跃判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionCount++;
        jumpCount = jumpMaxCount;//落地重置跳跃次数

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionCount--;
        
    }

}
