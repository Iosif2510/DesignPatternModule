using System;
using System.Collections.Generic;
using UnityEngine;

namespace _2510.DesignPatternModule.StateMachine
{
    public abstract class StateMachine<TStateEnum> where TStateEnum : Enum
    {
        protected Dictionary<TStateEnum, IState<TStateEnum>> stateMachines = new();
        
        private bool _isTransitioning;

        public TStateEnum CurrentStateEnum { get; protected set; }
        public IState<TStateEnum> CurrentState => stateMachines.GetValueOrDefault(CurrentStateEnum);

        protected abstract TStateEnum InitialState { get; }
        
        public void AddState(TStateEnum key, IState<TStateEnum> state)
        {
            stateMachines ??= new Dictionary<TStateEnum, IState<TStateEnum>>();
            stateMachines.TryAdd(key, state);
        }
        
        public void RemoveState(TStateEnum key)
        {
            if (stateMachines == null || !stateMachines.ContainsKey(key)) return;
            stateMachines.Remove(key);
        }
        
        public void EnterMachine()
        {
            ChangeState(InitialState);
        }

        /// <summary>
        /// Executes transition to a new state.
        /// </summary>
        /// <param name="newState">Next State to start</param>
        public virtual void ChangeState(TStateEnum newState)
        {
            if (!stateMachines.ContainsKey(newState))
            {
                Debug.LogWarning($"State {newState} not found in the state machine.");
                return;
            }
            
            if (EqualityComparer<TStateEnum>.Default.Equals(CurrentStateEnum, newState))
            {
                Debug.LogWarning($"Trying to change to the same state: {newState}. No action taken.");
                return;
            }
            _isTransitioning = true;
            CurrentState?.Exit();

            CurrentStateEnum = newState;

            CurrentState?.Enter();
            _isTransitioning = false;
        }

        /// <summary>
        /// Method to be called on Unity's Update event.
        /// </summary>
        public void Update()
        {
            if (_isTransitioning) return;
            CurrentState?.Update();
        }

        /// <summary>
        /// Method to be called on Unity's FixedUpdate event.
        /// </summary>
        public void PhysicsUpdate()
        {
            if (_isTransitioning) return;
            CurrentState?.PhysicsUpdate();
        }

        /// <summary>
        /// Method to be called on custom update logic.
        /// </summary>
        /// <param name="deltaTime">Custom time delay between call</param>
        public void CustomDelayUpdate(float deltaTime)
        {
            if (_isTransitioning) return;
            CurrentState?.CustomDelayUpdate(deltaTime);
        }
    }
}