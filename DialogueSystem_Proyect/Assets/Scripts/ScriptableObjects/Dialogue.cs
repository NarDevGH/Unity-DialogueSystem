using UnityEngine;

[System.Serializable]

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogues")]
public class Dialogue : ScriptableObject
{
    public Sentence[] sentences;
}

[System.Serializable]
public class Sentence
{
    public string name;
    public Sprite image;
    public float tPerChar = .75f;
    [TextArea(3, 5)]
    public string sentence;
}