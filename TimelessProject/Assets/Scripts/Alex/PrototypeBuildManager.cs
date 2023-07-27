using QFSW.QC;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrototypeBuildManager : MonoBehaviour
{


    private void Start()
    {
        Debug.Log("SALAM ALEIKUM ANNA!");
        Debug.Log("WE HAVE 4 SCENES TO SHOW YOU");
        Debug.Log("CURRENT SCENE IS SHOWING PLAYER MOVEMENT");
        Debug.Log("YOU CAN TYPE movement-scene TO OPEN THIS SCENE");
        Debug.Log("dialogue-scene TO OPEN DIALOGUE SCENE");
        Debug.Log("combat-scene TO OPEN MELEE COMBAT SCENE");
        Debug.Log("stealth-scene TO OPEN STEALTH SCENE");
        Debug.Log("CLOSE/OPEN THIS WINDOW IS ESC");
    }   


    [Command("movement-scene")]
    private void LoadMovementScene()
    {
        SceneManager.LoadScene(0);
    }
    [Command("dialogue-scene")]
    private void LoadDialogueScene()
    {
        SceneManager.LoadScene(1);
    }

    [Command("combat-scene")]
    private void LoadCombatScene()
    {
        SceneManager.LoadScene(2);
    }

    [Command("stealth-scene")]
    private void LoadStealthScene()
    {
        SceneManager.LoadScene(3);
    }
}
