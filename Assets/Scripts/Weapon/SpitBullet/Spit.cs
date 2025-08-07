using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 史莱姆口水攻击
/// </summary>
public class Spit : MonoBehaviour
{
    [SerializeField, Tooltip("口水预制体")] private GameObject spitPrefab;

    public float shootTime = 1f;
    private float nextFireTime = 0;

    private void Update()
    {
        if (Input.GetMouseButton(1) && Time.time >= nextFireTime)
        {
            Attack();
        }
    }
    
    public void Attack()
    {
        Instantiate(spitPrefab,transform.position,transform.rotation);
        nextFireTime = Time.time + shootTime;
    }
}
