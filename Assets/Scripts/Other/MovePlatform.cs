using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ƶ�ƽ̨
/// </summary>
public class MovePlatform : MonoBehaviour
{
    [SerializeField]private Transform targetPoint;
    [SerializeField]private float moveSpeed;
    [SerializeField]private float waitTime;

    private Vector2 originalPosition;
    private void Start()
    {
        StartCoroutine(Move());
        originalPosition = transform.position;//��¼ԭʼ����
        
    }
    /// <summary>
    /// �����ƶ�
    /// </summary>
    /// <returns>����Ŀ����ȴ����뷵��</returns>
    IEnumerator Move()
    {
        while (true)
        {
            while (Vector2.Distance(transform.position, targetPoint.position) > 0.1)
            {

                transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed*Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);

            while (Vector2.Distance(transform.position, originalPosition) > 0.1)
            {
                transform.position = Vector2.MoveTowards(transform.position, originalPosition, moveSpeed*Time.deltaTime);
                yield return null;
            }
        }
    }
    /// <summary>
    /// ����ҽӴ���ƽ̨������ҳ�Ϊ���������ƽ̨�ƶ�
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.SetParent(transform);
            }
    }
    /// <summary>
    /// ����뿪ƽ̨��ȡ�����ӹ�ϵ
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
