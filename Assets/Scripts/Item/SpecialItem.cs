using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItem : ItemBase
{
    [SerializeField] private bool canRotate = false; // 是否自转
    [SerializeField] private float rotateSpeed = 180f; // 自转旋转速度
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
