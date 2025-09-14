using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float damageAmount = 10f;
    public float timeToDisable = 1f;

    private PlayerStatus player;
    private bool isTriggered = false;
    private Vector3 originPos;
    private void OnEnable()
    {
        originPos = transform.position;
        transform.Translate(0, 4.5f, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isTriggered)
        {
            player = collision.GetComponent<PlayerStatus>();
            player.TakeDamage(damageAmount);
            isTriggered = true;
            Invoke(nameof(DisableTrap), timeToDisable);
            
        }
    }
    private void DisableTrap()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        transform.position = originPos;
    }

}
