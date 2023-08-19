using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    private AudioSource audioSource;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("BGM");

        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }

        audioSource.volume = PlayerPrefs.GetFloat("volume");
        
        DontDestroyOnLoad(this.gameObject);
    }
}
