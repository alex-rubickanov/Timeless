using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("SET THE MAX HP")]
    [SerializeField] private float maxHealth;
    [Header("SET THE RUNNING SPEED")]
    [SerializeField] private float runSpeed;
    [Header("SET THE SPRINT SPEED")]
    [SerializeField] private float sprintSpeed;
    [Header("SET ROTATION SMOOTHNESS")] [Range(2,10)]
    [SerializeField] private int rotationFactorPerFrame = 2;
    [Header("SET THE JUMP HEIGHT")]
    [SerializeField] private float maxJumpHeight = 1.0f;
    [Header("SET THE JUMP TIME")]
    [SerializeField] private float maxJumpTime = 0.5f;
    [Header("SET THE GRAVITY")]
    [SerializeField] private float gravity = -9.8f;


    private float health;
    private CharacterController _characterController;
    private Vector3 _direction;
    private float yVelocity;
    public bool CanMove = true;
    private bool _isJumping;

    private float initialJumpVelocity;
    

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        health = maxHealth;
        
        SetupJumpingVariables();
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

        _characterController.Move((_direction * currentMoveSpeed  + new Vector3(0, yVelocity, 0) ) * Time.deltaTime);
    }

    private void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            yVelocity = -1f;
        }
        yVelocity += gravity * Time.deltaTime;

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
                yVelocity = initialJumpVelocity;
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

    private void SetupJumpingVariables()
    {
        float ti, timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    public bool IsRunning()
    {
        return _direction.magnitude != 0;
    }

    public bool IsSprinting()
    {
        return GameInput.Instance.IsSprintPressed();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (IsDead())
        {
            // DEAD
        }
        else
        {
            // TAKE DAMAGE ANIMATION
        }
        
    }

    private bool IsDead()
    {
        return health <= 0;
    }

    public bool IsGrounded()
    {
        return _characterController.isGrounded;
    }
}
