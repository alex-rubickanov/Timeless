using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem Instance { get; private set; }


    private Camera mainCamera;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }


    public void PlaySound(AudioClip audioClip, Vector3 point)
    {
        AudioSource.PlayClipAtPoint(audioClip, point);
    }

    public void PlaySound(AudioClip audioClip)
    {
        AudioSource.PlayClipAtPoint(audioClip, mainCamera.transform.position);
    }
}
