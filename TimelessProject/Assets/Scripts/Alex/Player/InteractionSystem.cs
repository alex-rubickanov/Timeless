using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private float distanceToInteract;

    private void Update()
    {
        RaycastHit hitInfo;
        if (!Physics.Raycast(transform.position + Vector3.up, transform.forward, out hitInfo, distanceToInteract)) return;
        
        if (hitInfo.collider.TryGetComponent(out IInteractable interactable))
        {
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                interactable.Interact();
            }
        }
        
        
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position + Vector3.up, transform.forward * distanceToInteract, Color.green);
    }
}
