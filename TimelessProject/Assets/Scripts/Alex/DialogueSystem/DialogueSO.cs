using System;
using UnityEngine;

[CreateAssetMenu()]
public class DialogueSO : ScriptableObject
{
    public SpeakerSO SpeakerOne;
    public SpeakerSO SpeakerTwo;

    public Sentence[] Sentences;

    private void AssignSpeakers()
    {
        foreach (Sentence sentence in Sentences)
        {
            sentence.SetSpeakerSO(sentence.IsFirstSpeaker ? SpeakerOne : SpeakerTwo);
        }
    }

    private void OnEnable()
    {
        AssignSpeakers();
    }
}
