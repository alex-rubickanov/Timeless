using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float damage;
    [SerializeField] private float maxHealth;
    [SerializeField] private float distanceToSpotPlayer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceToAttack;
    [SerializeField] private float attackTimeout;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private AudioClip enemyHitSound;

    private float _health;
    private EnemyStates _state;
    private Transform _playerTransform;
    private Vector3 _startPos;
    private float attackTimeoutTimer;
    
    public event Action OnEnemyAttack;
    public event Action OnEnemyTakeDamage;
    public event Action OnEnemyDie;

    private enum EnemyStates
    {
        Idle,
        FollowPlayer,
        Comeback,
        Combat,
        Dead
    }
    
    private void Start()
    {
        _playerTransform = GameObject.FindWithTag("Player").transform;
        
        _health = Mathf.RoundToInt(maxHealth);

        _startPos = transform.position;

        attackTimeoutTimer = attackTimeout;
    }
    
    private void Update()
    {
        Vector3 targetPos = _playerTransform.position;
        targetPos.y = transform.position.y;
        switch (_state)
        {
            case EnemyStates.Idle:
                if ((_playerTransform.position - transform.position).magnitude < distanceToSpotPlayer)
                {
                    _state = EnemyStates.FollowPlayer;
                }
                break;
            case EnemyStates.FollowPlayer:
                if ((GetDistanceToPlayer() < distanceToAttack))
                {
                    _state = EnemyStates.Combat;
                }
                else if (GetDistanceToPlayer() > distanceToSpotPlayer + 7.0f)
                {
                    _state = EnemyStates.Comeback;
                }
                else
                {
                    FollowPlayer();
                    transform.LookAt(targetPos);
                }
                break;
            case EnemyStates.Comeback:
                if (transform.position != _startPos)
                {
                    MoveToStartPosition();
                    _startPos.y = this.transform.position.y;

                    transform.LookAt(_startPos);
                }
                else
                {
                    _state = EnemyStates.Idle;
                }

                if (GetDistanceToPlayer() < distanceToSpotPlayer)
                {
                    _state = EnemyStates.FollowPlayer;
                }
                break;
            case EnemyStates.Combat:
                attackTimeoutTimer += Time.deltaTime;
                if (attackTimeoutTimer >= attackTimeout)
                {
                    OnEnemyAttack?.Invoke();
                    attackTimeoutTimer = 0;
                }
                
                
                transform.LookAt(targetPos);
                if ((GetDistanceToPlayer() > distanceToAttack + 1.0f))
                {
                    attackTimeoutTimer = attackTimeout;
                    _state = EnemyStates.FollowPlayer;
                }
                break;
            case EnemyStates.Dead:

                break;
            default:
                _state = EnemyStates.Idle;
                break;
        }        
    }

    private void FollowPlayer()
    {
        Vector3 target = _playerTransform.position;
        target.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    private float GetDistanceToPlayer()
    {
        return (_playerTransform.position - transform.position).magnitude;
    }

    private void MoveToStartPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, _startPos, 1.5f * moveSpeed * Time.deltaTime);
    }

    public bool IsMoving()
    {
        return _state is EnemyStates.FollowPlayer or EnemyStates.Comeback;
    }

    public void TakeDamage(float takenDamage)
    {
        _health -= takenDamage;
        AudioSystem.Instance.PlaySound(enemyHitSound, transform.position);
        
        if (IsDead())
        {
            OnEnemyDie?.Invoke();
            _state = EnemyStates.Dead;
            gameObject.GetComponent<Collider>().enabled = false;
            
        }
        else
        {
            OnEnemyTakeDamage?.Invoke();
        }
    }

    public bool IsDead()
    {
        return _health <= 0.0f;
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
}
