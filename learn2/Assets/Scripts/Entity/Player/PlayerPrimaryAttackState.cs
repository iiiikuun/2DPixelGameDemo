using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private float lastTimeAttacked;
    private float combowindow = 0.5f;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _anomBoolName) : base(_player, _stateMachine, _anomBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (player.comboCounter > 2 || Time.time >= lastTimeAttacked + combowindow)
            player.comboCounter = 0;

        player.anim.SetInteger("ComboCounter", player.comboCounter);

        player.SetVelocity(player.attackMovement[player.comboCounter].x * player.facingDir, player.attackMovement[player.comboCounter].y);

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .15f);

        player.comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
