using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string dialogueHead;

    [TextArea(3, 10)]
    public string[] dialogueSentence;
}
