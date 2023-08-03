using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private Transform beforeWaterRespawnPoint;
    
    private void Awake()
    {

        Instance = this;
    }

    public void RespawnPlayerBeforeWater(Transform playerTransform)
    {
        playerTransform.position = beforeWaterRespawnPoint.position;
        
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene(1);
    }
}
