using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialogueDisplayer : ADialogueDisplayer
{
    [SerializeField] private UnityEngine.UI.Image _charImagePlace;
    [SerializeField] private TMPro.TextMeshProUGUI _nameField;
    [SerializeField] private TMPro.TextMeshProUGUI _dialogueField;
    [SerializeField] private Animator _animator;

    public override Action OnDialogueEnd { get; set; }

    private Queue<Sentence> _sentences = new Queue<Sentence>();
    private Sentence _currentSentence;
    private bool _sentenceFullyDisplayed;

    public override void StartDialogue(Dialogue dialogue)
    {
        _sentences.Clear();
        EnqueueDialogueSentences(dialogue);

        StartCoroutine("DisplayDialogueRoutine");

        _animator.Play("StartDialogue");
    }


    private IEnumerator DisplayDialogueRoutine() 
    {
        while (_sentences.Count > 0) 
        {
            _currentSentence = _sentences.Dequeue();
            yield return StartCoroutine("DisplayCurrentSentenceRoutine");
        }

        _animator.Play("EndDialogue");
    }

    private IEnumerator DisplayCurrentSentenceRoutine()
    { 
        _nameField.text = _currentSentence.name + ":";
        _charImagePlace.sprite = _currentSentence.image;
        _dialogueField.text = "";
        _sentenceFullyDisplayed = false;
        
        Coroutine charsApperingEffectRoutine = StartCoroutine("CharactersApperingEffectRoutine");

        while (_sentenceFullyDisplayed == false)
        {
            yield return null;  // <-

            if (Input.GetMouseButtonDown(0))
            {
                StopCoroutine(charsApperingEffectRoutine);
                _dialogueField.text = _currentSentence.sentence;

                break;
            }

        }
            
        while (true) //  click to pass to the next sentence
        {
            yield return null; // <- to force continue at the next frame so the next sentence click doesnt get triggered by the skip CharactersApperingEffect click

            if (Input.GetMouseButtonDown(0)) 
            {
                break;
            }
        }
    }

    private IEnumerator CharactersApperingEffectRoutine() 
    {
        int currentCharIndex = 0;
        int sentenceLenght = _currentSentence.sentence.Length;

        while (currentCharIndex < sentenceLenght) 
        {
            _dialogueField.text += _currentSentence.sentence[currentCharIndex++];
            yield return new WaitForSeconds(_currentSentence.timerPerChar);
        }

        _sentenceFullyDisplayed = true;
    }

    public void OnEndDialogueAnim()
    {
        OnDialogueEnd();
    }

    private void EnqueueDialogueSentences(Dialogue dialogue)
    {
        foreach (Sentence sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }
    }

}
