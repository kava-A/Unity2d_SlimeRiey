using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float offset;
    void Update()
    {
        // ��ȡ������������
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       

        // ������������ķ�������
        Vector2 direction = mousePosition - (Vector2)transform.position;

        // ������ת�Ƕ�
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //angle-90ʹy�ᳯ�����
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+offset));
    }
}
