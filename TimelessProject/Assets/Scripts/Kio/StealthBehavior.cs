using UnityEngine;
using UnityEngine.SceneManagement;

public class StealthBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GuardScript.OnGuardHasSpottedPlayer += Respawn;
    }

    void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
