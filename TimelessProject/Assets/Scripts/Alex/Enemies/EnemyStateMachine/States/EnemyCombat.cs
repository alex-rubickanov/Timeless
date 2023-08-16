using UnityEngine;

public class EnemyCombat : EnemyBaseState
{
    private float attackTimer = 0;
    public override void EnterState(EnemyStateMachine enemy)
    {
        attackTimer = enemy.GetAttackTimeOut();
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        attackTimer += Time.deltaTime;
        
        if (attackTimer >= enemy.GetAttackTimeOut())
        {
            enemy.AttackEvent();
            attackTimer = 0;
        }
        
        if ((enemy.GetDistanceToPlayer() > enemy.GetDistanceToAttack() + 1.0f))
        {
            attackTimer = enemy.GetAttackTimeOut();
            enemy.SwitchState(enemy.FollowState);
        }
    }
}
