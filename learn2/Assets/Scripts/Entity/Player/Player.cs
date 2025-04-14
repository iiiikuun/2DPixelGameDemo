using System.Collections;
using UnityEngine;

public class Player : Entity
{
    [Header("攻击")]
    public Vector2[] attackMovement;
    public Vector2[] enemyMovement;
    public Transform[] attackCheck;
    public float[] attackCheckRadius;
    public int comboCounter;
    public float heavyAttackFreezeDuration;

    [Header("状态")]
    public bool isBusy = false;

    [Header("移动")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;

    [Header("技能")]
    public float skillDir;
    public bool isCounterAttacking;
    public SkillManager skill { get; private set; }
    public GameObject sword { get; private set; }

    [Header("碰撞")]
    [SerializeField] private Collider2D cd_Trigger;

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }
    public PlayerAimSwordState aimSword { get; private set; }
    public PlayerCatchSwordState catchSword { get; private set; }
    public PlayerBlackholeState blackHole { get; private set; }
    public PlayerDeadState deadState { get; private set; }


    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine(this);

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        aimSword = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSword = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        blackHole = new PlayerBlackholeState(this, stateMachine, "Jump");
        deadState = new PlayerDeadState(this, stateMachine, "Die");

        cd_Trigger.enabled = false;
    }

    protected override void Start()
    {
        base.Start();

        skill = SkillManager.instance;

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        SkillDirCheck();

        if (Input.GetKeyDown(KeyCode.K) && skill.dash.CanUseSkill())
            stateMachine.ChangeState(dashState);

        if (Input.GetKeyDown(KeyCode.F) && skill.crystal.CanUseSkill())
            skill.crystal.UseSkill();
    }

    #region 碰撞检测
    public void CD_Enabled()
    {
        cd_Trigger.enabled = true;
    }

    public void CD_Unenabled()
    {
        cd_Trigger.enabled = false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(attackCheck[0].position, attackCheckRadius[0]);
        Gizmos.color = new(1, 1, 1, 0.2f);
        Gizmos.DrawWireSphere(attackCheck[1].position, attackCheckRadius[1]);
        Gizmos.color = new(1, 1, 0.2f, 0.2f);
        Gizmos.DrawWireSphere(attackCheck[2].position, attackCheckRadius[2]);
    }
    #endregion
    #region 受击
    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }
    #endregion
    #region 状态
    public IEnumerator BusyFor(float _second)
    {
        isBusy = true;

        yield return new WaitForSeconds(_second);

        isBusy = false;
    }
    #endregion
    #region 技能
    private void SkillDirCheck()
    {
        skillDir = Input.GetAxisRaw("Horizontal");

        if (skillDir == 0)
            skillDir = facingDir;
    }

    public void AssignNewSword(GameObject _newSword)
    {
        sword = _newSword;
    }

    public void CatchTheSword()
    {
        stateMachine.ChangeState(catchSword);
        Destroy(sword);
    }

    public void SuccessfulCounterAttack() => counterAttack.SuccessfulCounterAttack();
    #endregion
    #region 动画
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    #endregion
}

