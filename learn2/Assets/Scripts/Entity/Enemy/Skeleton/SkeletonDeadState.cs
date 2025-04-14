using UnityEngine;

public class SkeletonDeadState : SkeletonState
{
    public SkeletonDeadState(Enemy_Skeleton _enemy, EnemyStateMachine _stateMachine, string _anomBoolName) : base(_enemy, _stateMachine, _anomBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.FreezeTime(true);
        enemy.cd.enabled = false;
        enemy.rb.gravityScale = 0;

        stateTimer = 1;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.rb.velocity = Vector2.zero;
    }
}
