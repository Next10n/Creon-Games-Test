using System;
using System.Collections.Generic;

namespace Code.Infrastructure
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IState>() {
                [typeof(BootstrapState)] = new BootstrapState(),
            };
        }

        public void Enter<TState>() where TState : IState
        {
            _states[typeof(TState)].Enter();
        }
    }
}