using System;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private float distanceToInteract;

    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.forward, out hit, distanceToInteract);
        
        
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, Vector3.forward * distanceToInteract, Color.green);
    }
}
