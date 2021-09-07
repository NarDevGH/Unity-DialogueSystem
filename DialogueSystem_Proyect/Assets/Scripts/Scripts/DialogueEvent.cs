using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private Dialogue _dialogue;
    void Start()
    {
        _dialogueManager.StartDialogue(_dialogue);
    }

    
}
