using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _anomBoolName) : base(_player, _stateMachine, _anomBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetZeroVelocity();

        stateTimer = Mathf.Infinity;

        player.skill.sword.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.anim.SetBool("ThrowSword", false);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyUp(KeyCode.Mouse1))
            player.anim.SetBool("ThrowSword", true);

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);

        Vector2 mousePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);

        player.FlipController(mousePosition.x, player.transform.position.x);
    }
}
