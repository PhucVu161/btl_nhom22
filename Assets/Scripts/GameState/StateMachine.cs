using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public static StateMachine instance;
    private void Awake()
    {
        instance = this;
    }


    public Breaktime breaktimeState;
    public Battle batleState;
    private GameState currentState;

    Dictionary<StateName, GameState> StateDict = new Dictionary<StateName, GameState>();

    void Start()
    {
        breaktimeState = new Breaktime();
        batleState = new Battle();
        StateDict.Add(StateName.Breaktime, breaktimeState);
        StateDict.Add(StateName.Battle, batleState);

        currentState = breaktimeState;
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }
    GameState GetStateByEnum(StateName stateName) => StateDict[stateName];
    public void ChangeState(StateName stateName)
    {
        currentState.OnExit();
        currentState = GetStateByEnum(stateName);
        currentState.OnEnter();
    }
    public void ChangeState(GameState state)
    {
        currentState.OnExit();
        currentState = state;
        currentState.OnEnter();
    }
}

public enum StateName
{
    Breaktime,
    Battle
}
public abstract class GameState
{
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate();
}