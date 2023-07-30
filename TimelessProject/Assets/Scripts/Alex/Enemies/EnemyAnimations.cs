using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator _animator;
    private Enemy _enemy;
    
    private int _animIDIsMoving;
    private int _animIDTakeDamage;
    private int _animIDDie;
    private int _animIDAttack;



    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        
        AssignAnimationsID();
        
        _enemy.OnEnemyAttack += PlayAnimationOnEnemyAttack;
        _enemy.OnEnemyTakeDamage += PlayAnimationOnEnemyTakeDamage;
        _enemy.OnEnemyDie += PlayAnimationOnEnemyDie;
    }
    
    private void Update()
    {
        PlayEnemyMovementAnimation();
    }

    private void PlayEnemyMovementAnimation()
    {
        _animator.SetBool(_animIDIsMoving, _enemy.IsMoving());
    }
    
    private void PlayAnimationOnEnemyAttack()
    {
        _animator.SetTrigger(_animIDAttack);
    }

    private void PlayAnimationOnEnemyDie()
    {
        _animator.SetTrigger(_animIDDie);
    }

    private void PlayAnimationOnEnemyTakeDamage()
    {
        _animator.SetTrigger(_animIDTakeDamage);
    }

    private void AssignAnimationsID()
    {
        _animIDIsMoving = Animator.StringToHash("isMoving");
        _animIDTakeDamage = Animator.StringToHash("TakeDamage");
        _animIDDie = Animator.StringToHash("Die");
        _animIDAttack = Animator.StringToHash("Attack");
    }
}
