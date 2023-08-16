using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        base.UpdateState(enemy);
        
        if ((enemy.GetPlayerPosition() - enemy.transform.position).magnitude < enemy.GetDistanceToSpotPlayer())
        {
            enemy.SwitchState(enemy.FollowState);
        }
    }
}
