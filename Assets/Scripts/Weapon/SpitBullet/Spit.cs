using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit : MonoBehaviour
{
    [SerializeField, Tooltip("��ˮԤ����")] private GameObject spitPrefab;

    public float shootTime = 1f;
    private float nextFireTime = 0;

    private void Update()
    {
        if (Input.GetKey(KeyCode.F) && Time.time >= nextFireTime)
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
