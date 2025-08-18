using System;
using System.Collections.Generic;

namespace _2510.DesignPatternModule.StateMachine
{
    public class Transition<TStateEnum> where TStateEnum : Enum

    {
    private TStateEnum _previousState;
    private TStateEnum _nextState;
    private StateMachine<TStateEnum> _machine;

    public Transition(StateMachine<TStateEnum> machine, TStateEnum previousState, TStateEnum nextState)
    {
        _machine = machine;
        _previousState = previousState;
        _nextState = nextState;
    }

    public void DoTransition()
    {
        if (EqualityComparer<TStateEnum>.Default.Equals(_machine.CurrentStateEnum, _nextState)) return;
        _machine.ChangeState(_nextState);
    }
    }
}