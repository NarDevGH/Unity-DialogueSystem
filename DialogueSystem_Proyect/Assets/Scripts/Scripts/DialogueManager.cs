using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image _characterImagePlace;
    [SerializeField] private TextMeshProUGUI _characterNamePlace;
    [SerializeField] private TextMeshProUGUI _dialoguePlace;
    [SerializeField] private Animator _dialoguePanelAnimator;

    private Queue<Sentence> _sentences = new Queue<Sentence>();
    private Sentence _currentSentence;

    private bool _dialogueOngoing;

    private void Update()
    {
        if (_dialogueOngoing) 
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                DisplayNextSentence();
            }
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

        _dialogueOngoing = true;

        DisplayNextSentence();
    }

    private void GetDialogueInformation(Dialogue dialogue)
    {
        foreach (Sentence sentence in dialogue.sentences)
        {
            _sentences.Enqueue(sentence);
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
        _dialoguePlace.text = _currentSentence.sentence;
    }

    private void EndDialogue() 
    {
        _dialogueOngoing = false;

        if (_dialoguePanelAnimator != null)
        {
            _dialoguePanelAnimator.Play("EndDialogue");
        }
    }
}
