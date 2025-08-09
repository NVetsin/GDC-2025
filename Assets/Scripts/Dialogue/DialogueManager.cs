using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> dialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogue = new Queue<string>();
    }

    public void startDialogue(Dialogue dialogue)
    {

    }
}
