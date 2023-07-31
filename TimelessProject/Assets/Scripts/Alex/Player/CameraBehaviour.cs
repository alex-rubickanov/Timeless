using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cameraAngleX;
    [SerializeField] private Vector3 positionOffset;

    [Range(1, 10)]
    [SerializeField] private int cameraSpeed;


    private void Start()
    {
        transform.position = target.position + positionOffset; 
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
