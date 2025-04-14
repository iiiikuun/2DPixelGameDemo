using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState { get; private set; }
    private Enemy enemy;

    public EnemyStateMachine(Enemy _enemy)
    {
        enemy = _enemy;
    }

    public void Initialize(EnemyState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(EnemyState _newState)
    {
        if (enemy.isFreezed)
            return;

        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
