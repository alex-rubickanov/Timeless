using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private PlayerController _playerController;
    private CombatSystem _combatSystem;
    private Animator _animator;

    private int _animIDIsRunning;
    private int _animIDIsSprinting;
    private int _animIDIsAttack;
    private int _animIDIsFalling;

    private void Awake()
    {
        AssignAnimationsID();
    }

    private void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _combatSystem = GetComponentInParent<CombatSystem>();
        _combatSystem.OnAttackEvent += PlayAttack;
        
        _animator = GetComponent<Animator>();
    }

    private void PlayAttack()
    {
        _animator.SetTrigger(_animIDIsAttack);
    }

    private void Update()
    {
        HandleAnimation();
    }

    private void HandleAnimation()
    {
        _animator.SetBool(_animIDIsRunning, _playerController.IsRunning());
        _animator.SetBool(_animIDIsSprinting, _playerController.IsSprinting());
        _animator.SetBool(_animIDIsFalling, !_playerController.IsGrounded());
    }

    private void AssignAnimationsID()
    {
        _animIDIsRunning = Animator.StringToHash("IsRunning");
        _animIDIsSprinting = Animator.StringToHash("IsSprinting");
        _animIDIsAttack = Animator.StringToHash("Attack");
        _animIDIsFalling = Animator.StringToHash("IsFalling");
    }
}
