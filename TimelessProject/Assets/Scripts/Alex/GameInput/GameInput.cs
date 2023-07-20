using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    
    private InputActions _inputActions;
    private bool _isSprintPressed;

    public Action OnAttackAction;
    
    private void Awake()
    {
        Instance = this;
        
        _inputActions = new InputActions();
        _inputActions.Player.Enable();

        _inputActions.Player.Sprint.started += SetSprint;
        _inputActions.Player.Sprint.canceled += SetSprint;
        
        _inputActions.Player.Attack.performed += OnAttack_performed;
        
    }

    private void OnAttack_performed(InputAction.CallbackContext obj)
    {
        OnAttackAction?.Invoke();
    }


    private void SetSprint(InputAction.CallbackContext context)
    {
        _isSprintPressed = context.ReadValueAsButton();
    }
    public Vector2 GetMovementVector()
    {
        return _inputActions.Player.Move.ReadValue<Vector2>();
    }

    public bool IsSprintPressed()
    {
        return _isSprintPressed;
    }
}
