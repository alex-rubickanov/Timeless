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
        if (SpeakerOne == null || SpeakerTwo == null || Sentences == null) return;
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
