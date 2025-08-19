using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBasic : MonoBehaviour
{
   [SerializeField] protected string npcName;
   [SerializeField] protected NPCType npcType;
   [SerializeField] protected List<string> talkContents; //�Ի�����
    protected bool canTalk = false;
    protected bool isInteract = false; 
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private float lr;//���ڼ�������ҵĺ������꣬�жϴ�����ҵ�����
    protected virtual void Update()
    {
        lr = transform.position.x - player.transform.position.x;//�������������ĺ�������
        //ͨ�����������жϳ���
        if (lr < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteract = true;
        }
        else
        {
            isInteract = false;
        }
    }
    
}
public enum NPCType
{
    Villager,
    Healer,
    Special,
}
