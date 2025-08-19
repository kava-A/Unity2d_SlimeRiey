using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "ScriptableObjects/DialogData", order = 1)]
public class DialogData : ScriptableObject
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>(); // 对话行列表
}

[System.Serializable]
public class DialogueLine
{
    public string speakerName; // 说话人名字
    [TextArea(3, 10)] public string dialogueText; // 对话内容
    public List<DialogueOption> options; // 对话选项
    public bool isPlayerLine; // 是否是玩家的对话
}

[System.Serializable]
public class DialogueOption
{
    public string optionText; // 选项文本
    public int nextLineIndex; // 下一行对话索引
}
