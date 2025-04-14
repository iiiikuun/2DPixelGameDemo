using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public CharacterStats stats { get; private set; }

    #endregion

    [Header("眩晕")]
    public float stunDuration;
    public Vector2 stunDirection;
    [Header("霸体")]
    public bool isStoic;
    protected int stoicCounter;
    [Header("无敌")]
    public bool isInvincible;
    protected int invincibleCounter;

    [Header("碰撞检测")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    public System.Action onFlipped;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
        stats = GetComponent<CharacterStats>();
    }

    protected virtual void Update()
    {

    }
    #region 受击
    public virtual void Damage(bool _hitTarget, float _freezetime, Transform attackerTransform, Vector2 _vector2)
    {
        if (_hitTarget)
        {
            fx.StartCoroutine("FlashFX");

            if (attackerTransform != null && !isStoic)
            {
                if (transform.position.x >= attackerTransform.position.x)
                    rb.velocity = _vector2;
                else
                    rb.velocity = new Vector2(-_vector2.x, _vector2.y);
            }
        }
    }

    public virtual void BeStunned(float _stunDuration, Transform attackerTransform, Vector2 _stunDirection)
    {
        stunDuration = _stunDuration;

        if (transform.position.x >= attackerTransform.position.x)
            stunDirection = _stunDirection;
        else
            stunDirection = new Vector2(-_stunDirection.x, _stunDirection.y);
    }

    public virtual void Die()
    {

    }
    #endregion
    #region 状态

    public virtual void IStoic(bool isFreezing)
    {
        if (isFreezing)
            stoicCounter++;
        else
            stoicCounter--;

        if (stoicCounter > 0)
            isStoic = true;
        else
            isStoic = false;
    }

    public virtual IEnumerator IStoicFor(float _seconds)
    {
        IStoic(true);

        yield return new WaitForSeconds(_seconds);

        IStoic(false);
    }

    public virtual void IInvincible(bool isFreezing)
    {
        if (isFreezing)
            invincibleCounter++;
        else
            invincibleCounter--;

        if (invincibleCounter > 0)
            isInvincible = true;
        else
            isInvincible = false;
    }

    public virtual IEnumerator IInvincibleFor(float _seconds)
    {
        IInvincible(true);

        yield return new WaitForSeconds(_seconds);

        IInvincible(false);
    }
    #endregion
    #region 速度
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public virtual void SetZeroVelocity()
    {
        rb.velocity = new Vector2(0, 0);
    }

    #endregion
    #region 碰撞检测
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    #endregion
    #region 转向
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if (onFlipped != null)
            onFlipped();
    }

    public virtual void FlipController(float _x)
    {
        if (_x < 0 && facingRight)
            Flip();
        else if (_x > 0 && !facingRight)
            Flip();
    }

    public virtual void FlipController(float _x, float center)
    {
        if (_x < center && facingRight)
            Flip();
        else if (_x > center && !facingRight)
            Flip();
    }

    #endregion
}
