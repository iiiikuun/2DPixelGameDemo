using UnityEngine;

public class SkeletonBattleState : SkeletonState
{
    private Transform player;
    private int moveDir;

    public SkeletonBattleState(Enemy_Skeleton _enemy, EnemyStateMachine _stateMachine, string _anomBoolName) : base(_enemy, _stateMachine, _anomBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;

        stateTimer = enemy.battleTime;

        enemy.anim.speed = 2;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.anim.speed = 1;
    }

    public override void Update()
    {
        base.Update();

        if (enemy.playerEnterAttackRange() == null)
        {
            if (player.position.x > enemy.transform.position.x)
                moveDir = 1;
            else if (player.position.x < enemy.transform.position.x)
                moveDir = -1;

            enemy.SetVelocity(enemy.moveSpeed * moveDir * 2, rb.velocity.y);
        }
        else if (CanAttack())
            stateMachine.ChangeState(enemy.attackState);

        if (enemy.IsPlayerDetected())
            stateTimer = enemy.battleTime;
        else if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > enemy.coldDistance)
            stateMachine.ChangeState(enemy.idleState);
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            return true;
        }
        return false;
    }
}
