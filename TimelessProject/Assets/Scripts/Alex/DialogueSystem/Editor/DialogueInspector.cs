using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueSO))]
public class DialogueInspector : Editor
{
    private SerializedProperty speakerOne;
    private SerializedProperty speakerTwo;

    private SerializedProperty sentences;

    private void OnEnable()
    {
        speakerOne = serializedObject.FindProperty("SpeakerOne");
        speakerTwo = serializedObject.FindProperty("SpeakerTwo");

        sentences = serializedObject.FindProperty("Sentences");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(speakerOne);
        EditorGUILayout.PropertyField(speakerTwo);
        EditorGUILayout.PropertyField(sentences);
        
        serializedObject.ApplyModifiedProperties();
    }
}