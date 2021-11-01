using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueDisplayer 
{
    public void StartDialogue(Dialogue dialogue);

    public Action OnDialogueEnd { get; set; }
}
