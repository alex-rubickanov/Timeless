using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    
    [SerializeField] private Transform target;
    [SerializeField] private float cameraAngleX;
    [SerializeField] private Vector3 positionOffset;

    [Range(1, 10)]
    [SerializeField] private int cameraSpeed;

    [SerializeField] private bool useClamp;
    [SerializeField] private float minClampZPos;
    [SerializeField] private float maxClampZPos;
    [SerializeField] private float minClampXPos;
    [SerializeField] private float maxClampXPos;

    private float fixedYPosition;

    private void Start()
    {
        transform.position = target.position + positionOffset; 
        transform.eulerAngles = new Vector3(cameraAngleX, 0, 0 );
        fixedYPosition = target.position.y + positionOffset.y;
    }

    private void LateUpdate()
    {
        float smoothFollow = (float)cameraSpeed / 1000;

        Vector3 requiredPosition = target.position + positionOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, requiredPosition, smoothFollow);
        transform.position = useClamp ? 
            new Vector3(Mathf.Clamp(smoothedPosition.x, minClampXPos, maxClampXPos), fixedYPosition, Mathf.Clamp(smoothedPosition.z, minClampZPos, maxClampZPos)) : smoothedPosition;
        
    }
}
