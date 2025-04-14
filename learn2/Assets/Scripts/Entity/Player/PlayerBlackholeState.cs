using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackholeState : PlayerState
{
    private float flyTime=0.25f;
    private bool skillUsed;
    private float defaultGravity;

    public PlayerBlackholeState(Player _player, PlayerStateMachine _stateMachine, string _anomBoolName) : base(_player, _stateMachine, _anomBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        defaultGravity = rb.gravityScale;

        skillUsed = false;
        stateTimer=flyTime;
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();

        rb.gravityScale=defaultGravity;

        player.fx.MakeTransprent(false);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
            player.SetVelocity(0, 15);

        if(stateTimer < 0)
        {
            player.SetVelocity(0, -0.01f);

            if (!skillUsed)
            {
                player.skill.blackhole.UseSkill();
                skillUsed = true;
            }
        }

        if(player.skill.blackhole.SkillCompleted())
            stateMachine.ChangeState(player.airState);
    }
}
