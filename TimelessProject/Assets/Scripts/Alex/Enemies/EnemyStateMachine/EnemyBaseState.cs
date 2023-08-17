using System;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateMachine enemy);

    public abstract void UpdateState(EnemyStateMachine enemy);
}
