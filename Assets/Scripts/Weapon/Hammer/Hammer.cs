using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
	private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if(gameObject.activeSelf==false)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("IsAtk");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy= collision.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            enemy.Freeze();

        }
    }
}
