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
<<<<<<< HEAD
            if(enemy==null)
                return;
=======
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
            enemy.TakeDamage(damage);
            enemy.Freeze();

        }
    }
}
