using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private Button _exitButton;

    private void Start()
    {
        _exitButton = GetComponent<Button>();
        _exitButton.onClick.AddListener(() =>
        {
            Debug.Log("Quit");
            Application.Quit();
        });
    }
}
