using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
	[SerializeField, Tooltip("����ʱ��")] private float time;

    private void Start()
    {
        Destroy(gameObject,time);
    }
}
