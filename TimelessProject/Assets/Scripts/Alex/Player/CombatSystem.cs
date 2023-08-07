using System;
using Unity.VisualScripting;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private float meleeDamage;


    private PlayerController _playerController;
    private bool _canAttack = true;

    public Action OnAttackEvent;
    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        GameInput.Instance.OnAttackAction += OnAttackAction;    
    }

    private void OnAttackAction()
    {
        PlayAttack();
    }

    private void PlayAttack()
    {
        if (!_canAttack) return;
        
        OnAttackEvent?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnAttackStart()
    {
        _canAttack = false;
    }

    private void OnAttackEnd()
    {
        _canAttack = true;
    }

    private void OnAttack() // Called in animation
    {
        const int maxColliders = 10;
        Collider[] hits = new Collider[maxColliders];
        int numHits = Physics.OverlapSphereNonAlloc(attackPoint.position, attackRange, hits);
        // Damage enemies
        for (int i = 0; i < numHits; i++)
        {
            if (hits[i].TryGetComponent(out IDamagable damagedObject))
            {
                damagedObject.TakeDamage(meleeDamage);
            }
        }
    }
}