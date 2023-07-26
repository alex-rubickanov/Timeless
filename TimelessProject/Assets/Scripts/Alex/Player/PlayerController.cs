using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("SET THE RUNNING SPEED")]
    [SerializeField] private float runSpeed;
    [Header("SET THE SPRINT SPEED")]
    [SerializeField] private float sprintSpeed;
    [Header("SET ROTATION SMOOTHNESS")] [Range(2,10)]
    [SerializeField] private int rotationFactorPerFrame = 2;
    [Header("SET THE JUMP HEIGHT")]
    [SerializeField] private float jumpHeight;
    [Header("SET THE GRAVITY")]
    [SerializeField] private float gravity = -9.8f;
    
    private CharacterController _characterController;
    private Vector3 _direction;
    private float yMovement;
    public bool CanMove = true;
    [SerializeField] private bool _isJumping; 
    

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleGravity();
        HandleJumping();
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (!CanMove) return;
        
        float currentMoveSpeed = GameInput.Instance.IsSprintPressed() ? sprintSpeed : runSpeed;
        if (_isJumping) currentMoveSpeed = runSpeed;
        
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        _direction = new Vector3(inputVector.x, 0, inputVector.y).normalized;

        _characterController.Move((_direction * currentMoveSpeed  + new Vector3(0, yMovement, 0) ) * Time.deltaTime);
    }

    private void HandleGravity()
    {
        yMovement += gravity * Time.deltaTime;
    }

    private void HandleJumping()
    {
        if (_characterController.isGrounded)
        {
            // player is grounded
            _isJumping = false;
            if(Input.GetKeyDown(KeyCode.Space))
            {
                // player jumps
                yMovement = jumpHeight;
            }
        }
        else
        {
            _isJumping = true;
        }
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
        return _direction.magnitude != 0;
    }

    public bool IsSprinting()
    {
        return GameInput.Instance.IsSprintPressed();
    }

    public bool IsGrounded()
    {
        return _characterController.isGrounded;
    }
}
