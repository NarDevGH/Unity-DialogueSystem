using System;
using UnityEngine;

public abstract class ADialogueDisplayer : MonoBehaviour
{
    public abstract void StartDialogue(Dialogue dialogue);

    public abstract Action OnDialogueEnd { get; set; }
}
