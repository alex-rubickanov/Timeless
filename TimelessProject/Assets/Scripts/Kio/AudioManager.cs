using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] GameObject audioObject;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject pauseMenu;
    private AudioSource audioSource;
    private float MusicVolume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        audioObject = GameObject.FindWithTag("BGM");
        audioSource = audioObject.GetComponent<AudioSource>();

        MusicVolume = PlayerPrefs.GetFloat("volume");
        audioSource.volume = MusicVolume;
        volumeSlider.value = MusicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = MusicVolume;
        PlayerPrefs.SetFloat("volume", MusicVolume);
    }

    public void VolumeChange(float volume)
    {
        MusicVolume = volume;
    }

    public void VolumeReset()
    {
        PlayerPrefs.DeleteKey("volume");
        audioSource.volume = 0.5f;
        volumeSlider.value = 0.5f;
    }

    public void Back()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
