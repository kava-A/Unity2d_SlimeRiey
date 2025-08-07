using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleNPC : NPCBasic
{
    public GameObject specialItem;
    private bool canTalk = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = true;
        }
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.E)&&canTalk)
        {
            UIManager.Instance.SetTalkContent(npcName, talkContents);
        }
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTalk = false;
            UIManager.Instance.talkPanel.SetActive(false);
        }
    }
   
}