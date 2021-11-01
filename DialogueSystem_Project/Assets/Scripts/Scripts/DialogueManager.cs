using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private ADialogueDisplayer _dialogueDisplayer;
    public ADialogueDisplayer DialogueDisplayer { get; set; }

    private bool _dialogueOngoing;
    public bool DialogueOnGoing { get { return _dialogueOngoing; } }

    private Queue<Dialogue> _dialoguesQueue;
    public static DialogueManager instance { get; set; }

    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(this);
        }

        instance = this;

        _dialoguesQueue = new Queue<Dialogue>();
        _dialogueOngoing = false;
        _dialogueDisplayer.OnDialogueEnd += DisplayNextDialogue;

    }

    public void AddDialogueToQueue(Dialogue dialogue) 
    {
        _dialoguesQueue.Enqueue(dialogue);

        if (_dialogueOngoing == false)
        {
            _dialogueOngoing = true;
            _dialogueDisplayer.StartDialogue(_dialoguesQueue.Dequeue());
        }

    }

    private void DisplayNextDialogue() 
    {
        if (_dialoguesQueue.Count == 0) 
        {
            _dialogueOngoing = false;
            return; 
        }

        _dialogueDisplayer.StartDialogue(_dialoguesQueue.Dequeue());
    }
}
