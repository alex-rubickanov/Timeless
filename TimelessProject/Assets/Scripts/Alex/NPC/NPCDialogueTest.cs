using UnityEngine;

public class NPCDialogueTest : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueSO _dialogueSO;
    
    public void Interact()
    {
        Dialogue.Instance.StartDialogue(_dialogueSO);
    }
}
