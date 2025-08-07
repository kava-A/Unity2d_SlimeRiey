using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(DistanceJoint2D))]
public class Ruk : MonoBehaviour
{
    [Header("抓钩设置")]
    public float hookRange = 10f; // 抓钩最大射程
    public LayerMask grapplableLayers; // 可抓钩的图层
    public float swingForce = 5f; // 摆动力量
    public float retractSpeed = 20f; // 回收速度

    [Header("引用")]
    public Transform firePoint; // 发射点
    public Rigidbody2D playerRb; // 玩家刚体

    // 组件
    private LineRenderer lineRenderer;
    private DistanceJoint2D distanceJoint;
    private bool isGrappling = false;
    private Vector2 hookPoint; // 抓钩命中点

    void Awake()
    {
        // 获取组件
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint = GetComponent<DistanceJoint2D>();

        // 初始禁用关节
        distanceJoint.enabled = false;
        // 设置线段渲染器的起点数量
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // 按下抓钩键（示例使用鼠标左键）
        if (Input.GetKeyDown(KeyCode.E) && !isGrappling)
        {
            FireGrapple();
        }
        // 松开抓钩键
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            RetractGrapple();
        }

        // 更新抓钩线
        if (isGrappling)
        {
            UpdateGrappleLine();

            // 抓钩摆动控制
            if (Input.GetKey(KeyCode.A))
            {
                playerRb.AddForce(-transform.right * swingForce);
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerRb.AddForce(transform.right * swingForce);
            }
        }
    }

    // 发射抓钩
    void FireGrapple()
    {
        // 计算鼠标方向
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)firePoint.position).normalized;

        // 射线检测
        var raycast = Physics2D.Raycast(firePoint.position, direction, hookRange, grapplableLayers);

        if (raycast)
        {
            // 命中可抓钩的物体
            hookPoint = raycast.point;
            isGrappling = true;

            // 设置关节
            distanceJoint.enabled = true;
            distanceJoint.connectedAnchor = hookPoint;
            distanceJoint.anchor = transform.InverseTransformPoint(firePoint.position);

            // 启用线段渲染器
            lineRenderer.enabled = true;
            UpdateGrappleLine();
        }
    }

    // 回收抓钩
    void RetractGrapple()
    {
        isGrappling = false;
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    // 更新抓钩线显示
    void UpdateGrappleLine()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, hookPoint);

        // 逐渐缩短关节距离，实现回收效果
        if (distanceJoint.distance > 0.5f)
        {
            distanceJoint.distance -= retractSpeed * Time.deltaTime;
        }
    }

    // 绘制Gizmos辅助线
    void OnDrawGizmosSelected()
    {
        if (firePoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(firePoint.position, hookRange);
        }
    }
}

