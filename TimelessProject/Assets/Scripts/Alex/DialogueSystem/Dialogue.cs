using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public static Dialogue Instance { get; private set; }
    
    // Left Speaker
    [Space(10)]
    [SerializeField] private TextMeshProUGUI speakerOneName;
    [SerializeField] private GameObject speakerOneSprite;
    
    // Right Speaker
    [Space(10)]
    [SerializeField] private TextMeshProUGUI speakerTwoName;
    [SerializeField] private GameObject speakerTwoSprite;
    
    [Space(10)]
    [SerializeField] private TextMeshProUGUI textField;
    
    private DialogueSO currentDialogue;
    private int currentSentenceIndex;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CloseDialogueWindow();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Step();
        }

        ShowDialogue();
    }

    private void ShowDialogue()
    {
        if (currentDialogue.Sentences[currentSentenceIndex].IsFirstSpeaker)
        {
            ShowLeftSpeaker();
        }
        else
        {
            ShowRightSpeaker();
        }

        textField.text = currentDialogue.Sentences[currentSentenceIndex].sentenceText;
    }

    private void ShowLeftSpeaker()
    {
        speakerOneName.gameObject.SetActive(true);
        speakerOneSprite.SetActive(true);
        
        speakerTwoName.gameObject.SetActive(false);
        speakerTwoSprite.SetActive(false);
    }

    private void ShowRightSpeaker()
    {
        speakerTwoName.gameObject.SetActive(true);
        speakerTwoSprite.SetActive(true);
        
        speakerOneName.gameObject.SetActive(false);
        speakerOneSprite.SetActive(false);
    }
    
    public void StartDialogue(DialogueSO dialogueSO)
    {
        currentDialogue = dialogueSO;
        
        OpenDialogueWindow();

        AssignSpeakersData(dialogueSO);

        currentSentenceIndex = 0;
    }

    private void AssignSpeakersData(DialogueSO dialogueSO)
    {
        speakerOneName.text = dialogueSO.SpeakerOne.Name;
        speakerOneSprite.GetComponent<Image>().sprite = dialogueSO.SpeakerOne.Sprite;

        speakerTwoName.text = dialogueSO.SpeakerTwo.Name;
        speakerTwoSprite.GetComponent<Image>().sprite = dialogueSO.SpeakerTwo.Sprite;
    }

    private void Step()
    {
        if (currentDialogue.Sentences.Length - 1 == currentSentenceIndex)
        {
            CloseDialogueWindow();
        }
        else
        {
            currentSentenceIndex++;
        }
    }

    
    
    private void OpenDialogueWindow()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
    private void CloseDialogueWindow()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
