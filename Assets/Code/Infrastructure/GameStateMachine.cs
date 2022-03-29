using System;
using System.Collections.Generic;
using Code.Core;
using Code.StaticData;

namespace Code.Infrastructure
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;

        public GameStateMachine(
            IRectMeshGenerator rectMeshGenerator,
            IMeshCutter meshCutter,
            IClothFactory clothFactory,
            IClothConstraintService clothConstraintService,
            IStaticDataService staticDataService)
        {
            _states = new Dictionary<Type, IState>() {
                [typeof(BootstrapState)] = new BootstrapState(rectMeshGenerator, meshCutter, clothFactory, clothConstraintService, staticDataService),
            };
        }

        public void Enter<TState>() where TState : IState
        {
            _states[typeof(TState)].Enter();
        }
    }
}