using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private int meleeDamage;

    private ThirdPersonController playerController;
    private Animator animator;
    private const string ATTACK_TRIGGER = "Attack";

    private bool canAttack = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(canAttack)
            {
                Attack();
            }
        }

    }

    private void Attack()
    {
        // Play animation
        animator.SetTrigger(ATTACK_TRIGGER);
        //Other logic will be played on Animation Event!
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    #region Animation Events
    private void OnAttackStart()
    {
        canAttack = false;
    }

    private void OnAttackEnd()
    {
        canAttack = true;
    }

    private void OnAttack() // Called in animation
    {
        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange);
        // Damage enemies
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<IDamagable>(out IDamagable damagedObject))
            {
                damagedObject.TakeDamage(meleeDamage);
            }
        }
    }
    #endregion
}
