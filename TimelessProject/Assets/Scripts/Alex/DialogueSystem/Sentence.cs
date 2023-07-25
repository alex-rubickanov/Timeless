using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Sentence
{
    private SpeakerSO _speaker;
    public string sentenceText;
    
    public bool IsFirstSpeaker;
    
    
    public void SetSpeakerSO(SpeakerSO speaker)
    {
        _speaker = speaker;
    }

    public SpeakerSO GetSpeakerSO()
    {
        return _speaker;
    }
    
    
}