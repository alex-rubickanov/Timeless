using System;
using UnityEngine;

public class NPCDialogueTest : MonoBehaviour
{
    [SerializeField] private DialogueSO _dialogueSO;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    private void Interact()
    {
        Dialogue.Instance.StartDialogue(_dialogueSO);
    }
}
