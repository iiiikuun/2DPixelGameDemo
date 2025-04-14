using UnityEngine;

public class SkeletonAttackState : SkeletonState
{
    public SkeletonAttackState(Enemy_Skeleton _enemy, EnemyStateMachine _stateMachine, string _anomBoolName) : base(_enemy, _stateMachine, _anomBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
