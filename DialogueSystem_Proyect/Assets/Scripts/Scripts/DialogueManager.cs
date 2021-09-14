using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    
    [SerializeField] private UnityEngine.UI.Image _characterImagePlace;
    [SerializeField] private TMPro.TextMeshProUGUI _characterNamePlace;
    [SerializeField] private TMPro.TextMeshProUGUI _dialoguePlace;
    [SerializeField] private Animator _dialoguePanelAnimator;

    private Queue<Sentence> _sentences = new Queue<Sentence>();
    private Sentence _currentSentence;

    private bool _dialogueOngoing;
    private bool _sentencesFullyDisplayed;

    private int _currentSentenceCharIndex;

    private float _currentTimePerCharacter;
    private float _characterTimer;

    private void Start()
    {
        _dialogueOngoing = false;
        _sentencesFullyDisplayed = false;
    }

    private void Update()
    {
        if (_dialogueOngoing) 
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                if (_sentencesFullyDisplayed)
                {
                    DisplayNextSentence();
                    _sentencesFullyDisplayed = false;
                }
                else 
                {
                    _sentencesFullyDisplayed = true;
                    _currentSentenceCharIndex = 0;
                    _dialoguePlace.text = _currentSentence.sentence;
                }
            }

            if (_sentencesFullyDisplayed == false) 
            {
                if (_characterTimer <= 0)
                {
                    if (_currentSentenceCharIndex < _currentSentence.sentence.Length)
                    {
                        _characterTimer = _currentTimePerCharacter;
                        _dialoguePlace.text += _currentSentence.sentence[_currentSentenceCharIndex++];
                    }
                    else 
                    {
                        _sentencesFullyDisplayed = true;
                        _currentSentenceCharIndex = 0;
                    }
                }
                else 
                {
                    _characterTimer -= Time.unscaledDeltaTime;
                }
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

    public void StartDialogue(Dialogue dialogue)
    {
        _sentences.Clear();

        GetDialogueInformation(dialogue);

        if (_dialoguePanelAnimator != null)
        {
            _dialoguePanelAnimator.Play("StartDialogue");
        }

        DisplayNextSentence();

        _dialogueOngoing = true;
    }

    private void EndDialogue()
    {
        _dialogueOngoing = false;

        if (_dialoguePanelAnimator != null)
        {
            _dialoguePanelAnimator.Play("EndDialogue");
        }
    }

    private void DisplayNextSentence() 
    {
        if (_sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        _currentSentence = _sentences.Dequeue();
        _characterNamePlace.text = _currentSentence.name + ":";
        _characterImagePlace.sprite = _currentSentence.image;
        _dialoguePlace.text = "";

        _currentTimePerCharacter = _currentSentence.tPerChar;
        _characterTimer = _currentTimePerCharacter;
    }

    
}
