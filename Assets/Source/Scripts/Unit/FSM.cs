using System;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Units
{
    public class FSM
    {
        private readonly Dictionary<Type, IRunState> states;
        private IRunState current;

        public FSM(Dictionary<Type, IRunState> states)
        {
            this.states = states;
        }

        public void Enter<State, T>(T argument) where State : class, IEnterState<T>
        {
            State state = ChangeState<State>();
            state.Enter(argument);
        }

        public void Enter<State>() where State : class, IEnterState
        {
            State state = ChangeState<State>();
            state.Enter();
        }

        public void Run()
        {
            current?.Run();
        }

        private State ChangeState<State>() where State : class, IRunState
        {
            current?.Exit();
            State state = GetState<State>();
            current = state;
            return state;
        }

        private State GetState<State>() where State : class, IRunState
        {
            return states[typeof(State)] as State;
        }
    }


    public interface IRunState
    {
        void Run();
        void Exit();
    }

    public interface IEnterState : IRunState
    {
        void Enter();
    }

    public interface IEnterState<TParameter> : IRunState
    {
        void Enter(TParameter argument);
    }
}