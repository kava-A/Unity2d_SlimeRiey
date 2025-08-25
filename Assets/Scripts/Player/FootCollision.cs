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
<<<<<<< HEAD
            Enemy enemy=collision.GetComponent<Enemy>();
            if(enemy==null)//防止敌人死亡时脚本报错
                return;
            enemy.TakeDamage(damage);
=======
            collision.GetComponent<Enemy>().TakeDamage(damage);
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
        }
    }
}
