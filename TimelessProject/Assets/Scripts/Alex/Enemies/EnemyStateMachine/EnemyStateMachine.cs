using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour, IDamagable
{
     
    private EnemyBaseState currentState;
    
    public readonly EnemyIdle IdleState = new EnemyIdle();
    public readonly EnemyFollowPlayer FollowState = new EnemyFollowPlayer();
    public readonly EnemyComeback ComebackState = new EnemyComeback();
    public readonly EnemyCombat CombatState = new EnemyCombat();
    public readonly EnemyDead DeadState = new EnemyDead();

    [SerializeField] private Transform attackPoint;
    [Space(5)]    
    [SerializeField] private float health;
    [SerializeField] private float damage;
    [SerializeField] private float distanceToSpotPlayer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceToAttack;
    [SerializeField] private float attackTimeOut;
    [SerializeField] private float attackRange;

    private Vector3 startPos;
    private Transform playerTransform;
    private Collider collider;

    public event Action OnEnemyAttack;
    public event Action OnEnemyTakeDamage;
    public event Action OnEnemyDie;
    private void Awake()
    {
        startPos = transform.position;
        
        playerTransform = GameObject.FindWithTag("Player").transform;
        collider = GetComponent<Collider>();
    }

    private void Start()
    {
        currentState = IdleState;
        
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
        Debug.Log(currentState);
    }

    public void SwitchState(EnemyBaseState newState)
    {
        currentState = newState;
        
        currentState.EnterState(this);
    }
    
    private void OnAttackAnimationEvent()
    {
        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRange);
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out PlayerController playerController))
            {
                playerController.TakeDamage(damage);
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public float GetHealth()
    {
        return health;
    }

    public void TakeDamage(float receivedDamage)
    {
        health -= receivedDamage;
        OnEnemyTakeDamage?.Invoke();
    }

    public bool IsDead()
    {
        throw new NotImplementedException();
    }

    public Vector3 GetStartPosition()
    {
        return startPos;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerTransform.position;
    }

    public float GetDistanceToSpotPlayer()
    {
        return distanceToSpotPlayer;
    }

    public float GetDistanceToPlayer()
    {
        return (playerTransform.position - transform.position).magnitude;
    }

    public float GetDistanceToAttack()
    {
        return distanceToAttack;
    }

    public void AttackEvent()
    {
        OnEnemyAttack?.Invoke();
    }

    public void DieEvent()
    {
        OnEnemyDie?.Invoke();
    }

    public float GetAttackTimeOut()
    {
        return attackTimeOut;
    }

    public bool IsMoving()
    {
        return currentState == ComebackState || currentState == FollowState;
    }

    public void DisableCollider()
    {
        collider.enabled = false;
    }
}