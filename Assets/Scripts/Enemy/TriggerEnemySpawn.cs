using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemySpawn : MonoBehaviour
{
	public Transform position;
    private bool isTriggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (position == null) return;
        if (collision.CompareTag("Player")&&!isTriggered)
        {
            EventDefine.CallEnemySpawnEvent(position);
            isTriggered = true;
        }
    }
}
