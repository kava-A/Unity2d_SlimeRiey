using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(DistanceJoint2D))]
public class Ruk : MonoBehaviour
{
    [Header("ץ������")]
    public float hookRange = 10f; // ץ��������
    public LayerMask grapplableLayers; // ��ץ����ͼ��
    public float swingForce = 5f; // �ڶ�����
    public float retractSpeed = 20f; // �����ٶ�

    [Header("����")]
    public Transform firePoint; // �����
    public Rigidbody2D playerRb; // ��Ҹ���

    // ���
    private LineRenderer lineRenderer;
    private DistanceJoint2D distanceJoint;
    private bool isGrappling = false;
    private Vector2 hookPoint; // ץ�����е�

    void Awake()
    {
        // ��ȡ���
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint = GetComponent<DistanceJoint2D>();

        // ��ʼ���ùؽ�
        distanceJoint.enabled = false;
        // �����߶���Ⱦ�����������
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // ����ץ������ʾ��ʹ����������
        if (Input.GetKeyDown(KeyCode.E) && !isGrappling)
        {
            FireGrapple();
        }
        // �ɿ�ץ����
        else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
        {
            RetractGrapple();
        }

        // ����ץ����
        if (isGrappling)
        {
            UpdateGrappleLine();

            // ץ���ڶ�����
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

    // ����ץ��
    void FireGrapple()
    {
        // ������귽��
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)firePoint.position).normalized;

        // ���߼��
        var raycast = Physics2D.Raycast(firePoint.position, direction, hookRange, grapplableLayers);

        if (raycast)
        {
            // ���п�ץ��������
            hookPoint = raycast.point;
            isGrappling = true;

            // ���ùؽ�
            distanceJoint.enabled = true;
            distanceJoint.connectedAnchor = hookPoint;
            distanceJoint.anchor = transform.InverseTransformPoint(firePoint.position);

            // �����߶���Ⱦ��
            lineRenderer.enabled = true;
            UpdateGrappleLine();
        }
    }

    // ����ץ��
    void RetractGrapple()
    {
        isGrappling = false;
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    // ����ץ������ʾ
    void UpdateGrappleLine()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, hookPoint);

        // �����̹ؽھ��룬ʵ�ֻ���Ч��
        if (distanceJoint.distance > 0.5f)
        {
            distanceJoint.distance -= retractSpeed * Time.deltaTime;
        }
    }

    // ����Gizmos������
    void OnDrawGizmosSelected()
    {
        if (firePoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(firePoint.position, hookRange);
        }
    }
}

