using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    public void triggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().startDialogue(dialogue);
    }
}
