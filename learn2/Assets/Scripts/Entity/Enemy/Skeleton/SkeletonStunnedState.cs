using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : SkeletonState
{
    public SkeletonStunnedState(Enemy_Skeleton _enemy, EnemyStateMachine _stateMachine, string _anomBoolName) : base(_enemy, _stateMachine, _anomBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.fx.InvokeRepeating("RedColorBlink", 0, 0.1f);

        stateTimer=enemy.stunDuration;
        rb.velocity = enemy.stunDirection;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.fx.Invoke("CancelColorChange", 0);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer<0)
            stateMachine.ChangeState(enemy.battleState);
    }
}
