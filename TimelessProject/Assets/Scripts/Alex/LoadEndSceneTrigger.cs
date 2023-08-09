using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndSceneTrigger : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    [SerializeField] private string sceneNameToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}
