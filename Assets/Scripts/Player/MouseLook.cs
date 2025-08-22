using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float offset;
    void Update()
    {
        // 获取鼠标的世界坐标
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       

        // 计算玩家与鼠标的方向向量
        Vector2 direction = mousePosition - (Vector2)transform.position;

        // 计算旋转角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //angle-90使y轴朝向鼠标
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+offset));
    }
}
