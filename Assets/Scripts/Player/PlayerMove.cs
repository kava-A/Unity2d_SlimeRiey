using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;


    [Header("�ƶ�")]
    [SerializeField, Tooltip("�ƶ��ٶ�")] public float moveSpeed;
    [SerializeField, Tooltip("��Ծ��")] private float jumpForce;


    [Tooltip("��ײ�Ӵ�ͳ�ƣ�������Ծ�ж�")] private int collisionCount = 0;
    private Animator animator;
    private float inputX;
    private float lastInputX;//��¼�ϴκ�������ֵ
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

        //��Ծ
        if (Input.GetKeyDown(KeyCode.Space) && collisionCount > 0)
        {
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

    }

    /// <summary>
    /// ��ײͳ�ƣ���������ײ����
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
