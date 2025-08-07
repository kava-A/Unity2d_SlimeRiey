using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ʷ��ķ��ˮ����
/// </summary>
public class Spit : MonoBehaviour
{
    [SerializeField, Tooltip("��ˮԤ����")] private GameObject spitPrefab;

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
