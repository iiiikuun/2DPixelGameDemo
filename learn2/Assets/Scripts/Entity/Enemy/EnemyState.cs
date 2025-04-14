using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;

    protected Rigidbody2D rb;

    protected string anomBoolName;

    public float stateTimer { get; protected set; }
    protected bool triggerCalled;

    public EnemyState(EnemyStateMachine _stateMachine, string _anomBoolName)
    {
        this.stateMachine = _stateMachine;
        this.anomBoolName = _anomBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {

    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}

