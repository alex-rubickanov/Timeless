using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header("SET THE RUNNING SPEED")]
    [SerializeField] private float runSpeed;
    [Header("SET THE SPRINT SPEED")]
    [SerializeField] private float sprintSpeed;
    [Header("SET ROTATION SMOOTHNESS")] [Range(2,10)]
    [SerializeField] private int rotationFactorPerFrame = 2;

    private CharacterController _characterController;
    private Vector3 _direction;
    
    public bool CanMove = true;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (!CanMove) return;
        
        float currentMoveSpeed = GameInput.Instance.IsSprintPressed() ? sprintSpeed : runSpeed;
        
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        _direction = new Vector3(inputVector.x, 0, inputVector.y).normalized;

        _characterController.Move(_direction * currentMoveSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        if (_direction.magnitude == 0f) return;

        Vector3 positionToLookAt = new Vector3(_direction.x, 0, _direction.z);
        Quaternion currentRotation = transform.rotation;

        if (_direction.magnitude < 0.1f) return;
        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
    }

    public bool IsRunning()
    {
        return _direction.magnitude != 0 ? true : false;
    }

    public bool IsSprinting()
    {
        return GameInput.Instance.IsSprintPressed();
    }
}
