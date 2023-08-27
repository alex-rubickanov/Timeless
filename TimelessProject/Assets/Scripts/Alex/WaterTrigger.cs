using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    [SerializeField] private Transform beforeWaterRespawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            other.transform.position = beforeWaterRespawnPoint.position;
        }
    }
}    