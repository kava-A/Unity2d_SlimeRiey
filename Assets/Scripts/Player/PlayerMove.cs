using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;


    [Header("�ƶ�")]
    [SerializeField, Tooltip("�ƶ��ٶ�")] public float moveSpeed;
    [SerializeField, Tooltip("��Ծ��")] private float jumpForce;//������Ծ��
    [SerializeField,Tooltip("���ٱ���")]private float accelerate = 1.1f;
    [Header("���������")]
    public int jumpMaxCount = 2;
    public float exForce=70;//������


    [Tooltip("��ײ�Ӵ�ͳ�ƣ�������Ծ�ж�")] private int collisionCount = 0;
    private Animator animator;
    private float inputX;
    private float lastInputX;//��¼�ϴκ�������ֵ
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
        accelerate=Mathf.Clamp(accelerate,1,2);//���Ƽ��ٶ���1-2��֮��
        inputX = Input.GetAxis("Horizontal");
        lastInputX = inputX;
        Walk();

        //��Ծ
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
    /// ����ƶ�
    /// </summary>
    private void Walk()
    {
        //ͨ���ϴκ�������ֵ�ж��泯�ĸ�����
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
    /// ��ײͳ�ƣ�������Ծ�ж�
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionCount++;
        jumpCount = jumpMaxCount;//���������Ծ����

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collisionCount--;
        
    }

}
