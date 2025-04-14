using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _anomBoolName) : base(_player, _stateMachine, _anomBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        player.FlipController(xInput);

        if (xInput != 0&& !player.isBusy)
            stateMachine.ChangeState(player.moveState);

        base.Update();
    }
}
