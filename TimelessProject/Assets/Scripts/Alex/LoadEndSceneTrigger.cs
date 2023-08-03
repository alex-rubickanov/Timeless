using Cinemachine;
using UnityEngine;

public class LoadEndSceneTrigger : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            LevelManager.Instance.LoadEndScene();
        }
    }
}
