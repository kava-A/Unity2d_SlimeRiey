using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollision : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy=collision.GetComponent<Enemy>();
            if(enemy==null)//��ֹ��������ʱ�ű�����
                return;
            enemy.TakeDamage(damage);
        }
    }
}
