using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBasic : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] protected string npcName;
    [SerializeField] protected NPCType npcType;
    [SerializeField] protected List<string> talkContents; //�Ի�����
    protected bool canTalk = false;
    protected bool isInteract = false;
=======
   [SerializeField] protected string npcName;
   [SerializeField] protected NPCType npcType;
   [SerializeField] protected List<string> talkContents; //�Ի�����
    protected bool canTalk = false;
    protected bool isInteract = false; 
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
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
<<<<<<< HEAD

=======
    
>>>>>>> d8c932a3fe6f110359957261a3bf180bbac9c892
}
public enum NPCType
{
    Villager,
    Healer,
    Special,
}
