using UnityEngine;

public class EnemyComeback : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        Transform transform = enemy.transform;
        
        transform.LookAt(enemy.GetStartPosition());
        transform.position = 
            Vector3.MoveTowards(transform.position, enemy.GetStartPosition(), 1.5f * enemy.GetMoveSpeed() * Time.deltaTime);

        if (enemy.transform.position == enemy.GetStartPosition())
        {
            enemy.SwitchState(enemy.IdleState);
        }
        
        if (enemy.GetDistanceToPlayer() < enemy.GetDistanceToSpotPlayer())
        {
            enemy.SwitchState(enemy.FollowState);
        }
    }
}
