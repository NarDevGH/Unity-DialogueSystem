using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogueDisplayer : ADialogueDisplayer
{
    [SerializeField] private UnityEngine.UI.Image _charImagePlace;
    [SerializeField] private TMPro.TextMeshProUGUI _nameField;
    [SerializeField] private TMPro.TextMeshProUGUI _dialogueField;

    public override Action OnDialogueEnd { get; set; }

    private Queue<Sentence> _sentences = new Queue<Sentence>();
    private Sentence _currentSentence;

    public override void StartDialogue(Dialogue dialogue)
    {
        _sentences.Clear();

        GetDialogueInformation(dialogue);

        StartCoroutine("DisplayDialogueRoutine");
    }

    private IEnumerator DisplayDialogueRoutine() 
    {
        while (_sentences.Count > 0) 
        {
            _currentSentence = _sentences.Dequeue();
            yield return StartCoroutine("DisplayCurrentSentenceRoutine");
        }
    }

    private IEnumerator DisplayCurrentSentenceRoutine()
    {
        _nameField.text = _currentSentence.name + ":";
        _charImagePlace.sprite = _currentSentence.image;
        _dialogueField.text = _currentSentence.sentence;


        while (true) //  click to pass to the next sentence
        {
            yield return null; 

            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
        }
    }


    private void GetDialogueInformation(Dialogue dialogue)
    {
        foreach (Sentence sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }
    }
    private void EndDialogue()
    {
        _nameField.text = "";
        _dialogueField.text = "";
        _charImagePlace.sprite = null;

        OnDialogueEnd();
    }
}
