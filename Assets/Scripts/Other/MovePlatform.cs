using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自行移动平台
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
        originalPosition = transform.position;//记录原始坐标
        
    }
    /// <summary>
    /// 自行移动
    /// </summary>
    /// <returns>到达目标点后等待两秒返回</returns>
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
    /// 当玩家接触到平台，让玩家成为子物体跟随平台移动
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
    /// 玩家离开平台，取消父子关系
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
