using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 标枪投掷物
/// </summary>
public class JavelinArrow : Weapon
{
    private float destroyTime = 3f;
    private BoxCollider2D boxCollider2;
    private PolygonCollider2D polygonCollider2;
    private bool hasDamage = false;
    private void Start()
    {
        boxCollider2 = GetComponent<BoxCollider2D>();
        polygonCollider2 = GetComponent<PolygonCollider2D>();
        rb.velocity = transform.up * speed;
        StartCoroutine(UseGravity());
    }


    IEnumerator UseGravity()
    {
        yield return new WaitForSeconds(0.1f);
        if (boxCollider2 != null && boxCollider2.enabled == false)
            boxCollider2.enabled = true;
        //yield return new WaitForSeconds(0.1f);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.mass = 2;
        rb.gravityScale = 2f;
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !hasDamage && polygonCollider2.enabled)//避免多次伤害同一敌人
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Freeze();
                enemy.TakeDamage(damage);
                polygonCollider2.enabled = false;
                hasDamage = true;
            }
        }
    }
    /// <summary>
    /// 允许标枪插在物体上
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.gameObject.CompareTag("Player"))
        {
            string tag = collision.gameObject.tag;
            transform.SetParent(collision.transform);
            switch (tag)
            {
                case "Enemy":

                    rb.bodyType = RigidbodyType2D.Kinematic;
                    rb.gravityScale = 0f;
                    rb.angularVelocity = 0;
                    rb.velocity = Vector2.zero;
                    break;
                default:

                    rb.bodyType = RigidbodyType2D.Static;
                    break;
            }

        }
    }
}
