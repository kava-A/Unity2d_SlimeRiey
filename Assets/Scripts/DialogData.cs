using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "ScriptableObjects/DialogData", order = 1)]
public class DialogData : ScriptableObject
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>(); // �Ի����б�
}

[System.Serializable]
public class DialogueLine
{
    public string speakerName; // ˵��������
    [TextArea(3, 10)] public string dialogueText; // �Ի�����
    public List<DialogueOption> options; // �Ի�ѡ��
    public bool isPlayerLine; // �Ƿ�����ҵĶԻ�
}

[System.Serializable]
public class DialogueOption
{
    public string optionText; // ѡ���ı�
    public int nextLineIndex; // ��һ�жԻ�����
}
