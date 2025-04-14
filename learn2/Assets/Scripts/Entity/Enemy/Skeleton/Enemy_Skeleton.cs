using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    public CapsuleCollider2D cd { get; private set; }

    #region States
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunnedState stunnedState { get; private set; }
    public SkeletonDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "Idle");
        moveState = new SkeletonMoveState(this, stateMachine, "Move");
        battleState = new SkeletonBattleState(this, stateMachine, "Move");
        attackState = new SkeletonAttackState(this, stateMachine, "Attack");
        stunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned");
        deadState = new SkeletonDeadState(this, stateMachine, "Stunned");
    }

    protected override void Start()
    {
        base.Start();

        cd = GetComponent<CapsuleCollider2D>();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    #region 受击
    public override void BeStunned(float _stunDuration, Transform attackerTransform, Vector2 _stunDirection)
    {
        base.BeStunned(_stunDuration, attackerTransform, _stunDirection);

        if (!isStoic && !isInvincible)
            stateMachine.ChangeState(stunnedState);
    }


    public override void Damage(bool _hitTarget, float _freezetime, Transform attackerTransform, Vector2 _vector2)
    {
        base.Damage(_hitTarget, _freezetime, attackerTransform, _vector2);

        if (stateMachine.currentState == moveState || stateMachine.currentState == idleState)
            stateMachine.ChangeState(battleState);
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }
    #endregion
}

