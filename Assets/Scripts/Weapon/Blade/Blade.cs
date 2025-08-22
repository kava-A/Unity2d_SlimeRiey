using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : Weapon
{
    private Animator animator;
    private SpriteRenderer sprite;
    private void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float t = Mathf.PingPong(Time.time / 2, 1f);
        sprite.color=Color.Lerp(Color.red,Color.blue,t);
        if (gameObject.activeSelf == false)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("IsAtk");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
