using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItem : ItemBase
{
    [SerializeField] private bool canRotate = false; // �Ƿ���ת
    [SerializeField] private float rotateSpeed = 180f; // ��ת��ת�ٶ�
    private void Update()
    {
        if (canRotate)
        {

            transform.Rotate(rotateSpeed * Time.deltaTime, rotateSpeed * Time.deltaTime * 0.5f, rotateSpeed * Time.deltaTime * 0.2f);
        }
        else
        {
            return;
        }
    }
}
