using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    [SerializeField] private Dialogue _dialogue;

    void Start()
    {
        DialogueManager.instance.AddDialogueToQueue(_dialogue);
    }

    
}
