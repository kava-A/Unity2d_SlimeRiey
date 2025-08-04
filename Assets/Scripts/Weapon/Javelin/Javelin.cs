using System.Collections;
using UnityEngine;

/// <summary>
/// 标枪
/// </summary>
public class Javelin : MonoBehaviour
{
    [Tooltip("标枪预制体")]
    public GameObject javelinPrefab;
    [Tooltip("投掷点")]
    public Transform throwPoint;

    public float shootTime = 1f;
    private float nextFireTime=0;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) &&  Time.time >=nextFireTime)
        {
            Attack();
        }
    }
    // 实现攻击方法 - 投掷标枪
    public void Attack()
    {
        nextFireTime= Time.time + shootTime;
        // 实例化标枪
        Instantiate(javelinPrefab, throwPoint.position, throwPoint.rotation);
    }

}
