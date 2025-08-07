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
   [SerializeField] protected string[] talkContents; //对话内容

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private float lr;//用于计算与玩家的横向坐标，判断处于玩家的左右
    protected virtual void Update()
    {
        lr = transform.position.x - player.transform.position.x;//计算玩家与自身的横向坐标
        //通过横向坐标判断朝向
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
