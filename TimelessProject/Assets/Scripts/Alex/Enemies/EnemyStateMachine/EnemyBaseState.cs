using System;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateMachine enemy);

    public virtual void UpdateState(EnemyStateMachine enemy)
    {
        CheckDeath(enemy);
    }

    private void CheckDeath(EnemyStateMachine enemy)
    {
        if (enemy.GetHealth() <= 0)
        {
            enemy.SwitchState(enemy.DeadState);
        }
    }
}
