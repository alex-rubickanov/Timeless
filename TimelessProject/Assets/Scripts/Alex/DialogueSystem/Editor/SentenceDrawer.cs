using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Sentence))]
public class SentenceDrawer : PropertyDrawer
{
    private SerializedProperty _isFirstSpeaker;
    private SerializedProperty _sentenceText;

    private string speakerName;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label.text = label.text.Replace("Element", "Sentence");
        
        EditorGUI.BeginProperty(position, label, property);

        _isFirstSpeaker = property.FindPropertyRelative("IsFirstSpeaker");
        _sentenceText = property.FindPropertyRelative("sentenceText");
        
        Rect foldOutBox = new Rect(position.min.x, position.min.y,
            position.size.x, EditorGUIUtility.singleLineHeight);
        property.isExpanded = EditorGUI.Foldout(foldOutBox, property.isExpanded, label);

        if (property.isExpanded)
        {
            DrawSpeakerName(position);
            DrawSpeakerProperty(position);
            DrawFakeToggleProperty(position);
            DrawTextField(position);
        }

        
        EditorGUI.EndProperty();;
    }


    private void DrawSpeakerName(Rect position)
    {
        speakerName = _isFirstSpeaker.boolValue ? "Left Speaker" : "Right Speaker";
        
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight;
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;
        
        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.LabelField(drawArea, speakerName, EditorStyles.boldLabel);
    }
    
    private void DrawSpeakerProperty(Rect position)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * 2;
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;

        Rect drawArea = new Rect(xPos, yPos, width, height);
        EditorGUI.PropertyField(drawArea, _isFirstSpeaker, new GUIContent("Choose Speaker"));
    }

    private void DrawFakeToggleProperty(Rect position)
    {
        float xPos = position.max.x - 30.0f;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * 2;
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight;
        
        Rect drawArea = new Rect(xPos, yPos, width, height);
        
        EditorGUI.Toggle(drawArea, !_isFirstSpeaker.boolValue);
    }
    
    private void DrawTextField(Rect position)
    {
        float xPos = position.min.x;
        float yPos = position.min.y + EditorGUIUtility.singleLineHeight * 3;
        float width = position.size.x;
        float height = EditorGUIUtility.singleLineHeight * 5;
        
        Rect drawArea = new Rect(xPos, yPos, width, height);
        
        EditorGUI.PropertyField(drawArea, _sentenceText, new GUIContent("Sentence Text: "));
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int totalLines = 1;

        if (property.isExpanded)
        {
            totalLines += 8;
        }

        return (EditorGUIUtility.singleLineHeight * totalLines);
    }
    
}
 