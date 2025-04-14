using UnityEngine;

public class SkeletonGroundedState : SkeletonState
{
    public SkeletonGroundedState(Enemy_Skeleton _enemy, EnemyStateMachine _stateMachine, string _anomBoolName) : base(_enemy, _stateMachine, _anomBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected() || Vector2.Distance(PlayerManager.instance.player.transform.position, enemy.transform.position) < enemy.battleDistance)
            stateMachine.ChangeState(enemy.battleState);
    }
}
