using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProtectGame : MonoBehaviour
{
    private Transform safePoint;
    private void Start()
    {
        safePoint = transform.GetChild(0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "Player":
                collision.gameObject.transform.position = safePoint.position;
                break;
            case "Enemy":
                Destroy(collision.gameObject);
                break;
            default:
                
                break;
        }



    }
}
