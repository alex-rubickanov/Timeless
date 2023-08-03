using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            LevelManager.Instance.RespawnPlayerBeforeWater(other.transform);
        }
    }
}
