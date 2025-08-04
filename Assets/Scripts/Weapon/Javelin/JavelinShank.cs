using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinShank : MonoBehaviour
{ 
    private BoxCollider2D m_BoxCollider;
    private void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
    }
   
}
