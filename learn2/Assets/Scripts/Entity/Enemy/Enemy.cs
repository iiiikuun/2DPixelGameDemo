using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [Header("玩家检测")]
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("僵直")]
    public bool isFreezed;
    protected int freezeCounter;

    [Header("移动")]
    [SerializeField] protected float defaultMoveSpeed;
    public float moveSpeed;
    public float idleTime;
    public float moveTime;

    [Header("警戒")]
    public float viewDistance;
    public float battleDistance;
    public float battleTime;
    public float coldDistance;

    [Header("攻击")]
    public Transform attackCheck;
    public float attackCheckRadius;
    public float attackDistance;
    public float attackCooldown;
    [HideInInspector] public float lastTimeAttacked;

    [Header("死亡")]
    public float fadeDuration = 1;

    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine(this);

        moveSpeed = Random.Range(0.9f, 1.1f) * defaultMoveSpeed;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
    }

    #region 动画
    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    #endregion
    #region 碰撞检测
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(transform.position, Vector2.right * facingDir, viewDistance, whatIsPlayer);

    public virtual Collider2D playerEnterAttackRange()
    {
        return Physics2D.OverlapCircle(attackCheck.position, attackCheckRadius, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion
    #region 受击
    public override void Damage(bool _hitTarget, float _freezetime, Transform attackerTransform, Vector2 _vector2)
    {
        if (isInvincible)
            return;

        base.Damage(_hitTarget, _freezetime, attackerTransform, _vector2);

        if (_hitTarget && _freezetime != 0 && !isStoic)
            StartCoroutine("FreezeTimerFor", _freezetime);
    }

    public virtual void FreezeTime(bool isFreezing)
    {
        if (isFreezing)
            freezeCounter++;
        else
            freezeCounter--;

        if (freezeCounter > 0)
        {
            anim.speed = 0;
            isFreezed = true;
        }
        else
        {
            anim.speed = 1;
            isFreezed = false;
        }
    }

    public virtual IEnumerator FreezeTimerFor(float _seconds)
    {
        FreezeTime(true);

        yield return new WaitForSeconds(_seconds);

        FreezeTime(false);
    }
    public override void Die()
    {
        base.Die();

        Invoke("DestroyObject", fadeDuration);
        StartCoroutine(fx.FadeOut(fadeDuration));
    }

    protected virtual void DestroyObject()
    {
        Destroy(gameObject);
    }
    #endregion
    #region 速度
    public override void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isFreezed)
            return;

        base.SetVelocity(_xVelocity, _yVelocity);
    }

    public override void SetZeroVelocity()
    {
        if (isFreezed)
            return;

        base.SetZeroVelocity();
    }
    #endregion

}
