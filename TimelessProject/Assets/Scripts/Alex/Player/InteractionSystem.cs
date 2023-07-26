using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private float distanceToInteract;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hitInfo;
            if (!Physics.Raycast(transform.position, Vector3.forward, out hitInfo, distanceToInteract)) return;

            if (hitInfo.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(transform.position, Vector3.forward * distanceToInteract, Color.green);
    }
}
