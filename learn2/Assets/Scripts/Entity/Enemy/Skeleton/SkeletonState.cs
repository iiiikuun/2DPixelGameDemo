using UnityEngine;

public class SkeletonState : EnemyState
{
    protected Enemy_Skeleton enemy;

    public SkeletonState(Enemy_Skeleton _enemy, EnemyStateMachine _stateMachine, string _anomBoolName) : base(_stateMachine, _anomBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.anim.SetBool(anomBoolName, true);

        rb = enemy.rb;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.anim.SetBool(anomBoolName, false);
    }

    public override void Update()
    {
        base.Update();
    }
}
