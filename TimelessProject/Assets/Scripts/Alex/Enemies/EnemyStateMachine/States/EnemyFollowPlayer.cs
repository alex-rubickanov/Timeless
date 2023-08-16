using UnityEngine;

public class EnemyFollowPlayer : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        base.UpdateState(enemy);
        
        Vector3 target = enemy.GetPlayerPosition();
        Vector3 position = enemy.transform.position;
        
        target.y = position.y;
        
        enemy.transform.position = 
            Vector3.MoveTowards(position, target, enemy.GetMoveSpeed() * Time.deltaTime);
        
        enemy.transform.LookAt(target);

        if ((enemy.GetDistanceToPlayer() >= enemy.GetDistanceToSpotPlayer() + 7.0f))
        {
            enemy.SwitchState(enemy.ComebackState);
        }

        if (enemy.GetDistanceToPlayer() < enemy.GetDistanceToAttack())
        {
            enemy.SwitchState(enemy.CombatState);
        }
    }
}
