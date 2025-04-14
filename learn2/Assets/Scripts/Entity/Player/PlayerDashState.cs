public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _anomBoolName) : base(_player, _stateMachine, _anomBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.skill.dash.dashDuration;
        player.SetVelocity(player.skill.dash.dashSpeed * player.skillDir, 0);

        player.skill.dash.UseSkill();

        player.skill.dash.CloneOnDashStart();
        player.skill.dash.invincibleDuringDash();
        player.CD_Enabled();
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);

        player.skill.dash.CloneOnDashOver();
        player.skill.dash.invincibleDuringDash();
        player.CD_Unenabled();
        player.skill.dash.CritOnDashOver();
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.skill.dash.dashSpeed * player.facingDir, 0);

        player.fx.CreateAfterImage();

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
    }
}
