using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOBJ : MonoBehaviour
{
    
   [SerializeField] private List<GameObject> trap = new List<GameObject>();
    private bool isTriggered = false;
    private void Awake()
    {
        trap.Clear();
        
        GetAllChild(transform);
    }
    /// <summary>
    /// 获取所有子物体,包括孙物体
    /// </summary>
    /// <param name="parent">父物体</param>
    private void GetAllChild(Transform parent)
    {
        foreach (Transform child in parent)
        {
            trap.Add(child.gameObject);
            child.gameObject.SetActive(false);
            GetAllChild(child);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&&!isTriggered)
        {
            foreach (var item in trap)
            {
                item.SetActive(true);
            }
            isTriggered = true;
        }
    }
    
}
