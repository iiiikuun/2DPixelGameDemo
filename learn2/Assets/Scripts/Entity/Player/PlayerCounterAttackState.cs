using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _anomBoolName) : base(_player, _stateMachine, _anomBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.SetZeroVelocity();
        player.skill.parry.UseSkill();

        stateTimer = player.skill.parry.counterAttackDuration;

        player.anim.SetBool("SuccessfulCounterAttack", false);

        player.isCounterAttacking = true;
    }

    public override void Exit()
    {
        base.Exit();

        player.isCounterAttacking = false;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    public void SuccessfulCounterAttack()
    {
        player.anim.SetBool("SuccessfulCounterAttack", true);

        stateTimer = Mathf.Infinity;

        player.skill.parry.EnterParryState();
    }
}
