using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBasic : MonoBehaviour
{
    //public Action<NPCBasic> OnInteractionStart;
    //public Action<NPCBasic> OnInteractionEnd;
   [SerializeField] protected string npcName;
   [SerializeField] protected NPCType npcType;
   [SerializeField] protected string[] talkContents; //�Ի�����

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
    }
}
public enum NPCType
{
    Villager,
    Healer,
    Special,
}
