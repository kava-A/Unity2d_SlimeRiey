using System.Collections;
using UnityEngine;

/// <summary>
/// ��ǹ
/// </summary>
public class Javelin : MonoBehaviour
{
    [Tooltip("��ǹԤ����")]
    public GameObject javelinPrefab;
    [Tooltip("Ͷ����")]
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
    // ʵ�ֹ������� - Ͷ����ǹ
    public void Attack()
    {
        nextFireTime= Time.time + shootTime;
        // ʵ������ǹ
        Instantiate(javelinPrefab, throwPoint.position, throwPoint.rotation);
    }

}
