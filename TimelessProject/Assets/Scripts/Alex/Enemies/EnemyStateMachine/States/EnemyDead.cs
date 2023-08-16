using UnityEngine;

public class EnemyDead : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        enemy.DieEvent();
        enemy.DisableCollider();
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        
    }
}
