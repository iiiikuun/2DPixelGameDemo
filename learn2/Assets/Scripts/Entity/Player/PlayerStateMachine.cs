using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState {  get; private set; }
    private Player player;

    public PlayerStateMachine(Player _player)
    {
        player = _player;
    }


    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState _newState)
    {
        if (currentState == player.deadState)
            return;

        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
