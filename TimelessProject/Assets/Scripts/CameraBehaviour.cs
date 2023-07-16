using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cameraAngleX;

    [Range(1, 10)]
    [SerializeField] private int cameraSpeed;
    
    private Vector3 positionOffset;

    private void Awake()
    {
        positionOffset.x = 0;
        positionOffset.y = transform.position.y;
        positionOffset.z = transform.position.z;
    }

    private void Start()
    {
        transform.eulerAngles = new Vector3(cameraAngleX, 0, 0 );
    }

    private void LateUpdate()
    {
        float smoothFollow = (float)cameraSpeed / 1000;

        Vector3 targetPosition = target.position + positionOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothFollow);
        transform.position = smoothedPosition;    
    }
}
