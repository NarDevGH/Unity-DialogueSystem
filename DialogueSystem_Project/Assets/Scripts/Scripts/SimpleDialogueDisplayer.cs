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

    private void Awake()
    {
        InitDialogueVariables();
    }


    public override void StartDialogue(Dialogue dialogue)
    {
        GetDialogueInformation(dialogue);

        _animator.Play("StartDialogue");
    }

    public void OnStartDialogueAnim()
    {
        StartCoroutine("DisplayDialogueRoutine");
    }

    public void OnEndDialogueAnim()
    {
        InitDialogueVariables();

        OnDialogueEnd();
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
        #region INIT SentenceRoutine 
        _nameField.text = _currentSentence.name + ":";
        _charImagePlace.sprite = _currentSentence.image;
        _dialogueField.text = "";

        int currentCharIndex = 0;
        bool sentencesFullyDisplayed = false;
        double characterTimer = _currentSentence.timerPerChar;
        #endregion

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (sentencesFullyDisplayed)
                {
                    break;
                }
                else
                {
                    sentencesFullyDisplayed = true;
                    _dialogueField.text = _currentSentence.sentence;
                }
            }

            if (sentencesFullyDisplayed == false)
            {
                if (characterTimer <= 0)
                {
                    if (currentCharIndex < _currentSentence.sentence.Length)
                    {
                        characterTimer = _currentSentence.timerPerChar;
                        _dialogueField.text += _currentSentence.sentence[currentCharIndex++];
                    }
                    else
                    {
                        sentencesFullyDisplayed = true;
                        _dialogueField.text = _currentSentence.sentence;
                    }
                }
                else
                {
                    characterTimer -= Time.unscaledDeltaTime;
                }
            }

            yield return null;
        }
    }


    private void GetDialogueInformation(Dialogue dialogue)
    {
        foreach (Sentence sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }
    }

    private void InitDialogueVariables()
    {
        _nameField.text = "";
        _dialogueField.text = "";
        _charImagePlace.sprite = null;

        _sentences.Clear();
    }
}
