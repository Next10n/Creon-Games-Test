using System;
using System.Collections.Generic;
using Code.Core;
using Code.SceneManagement;
using Code.StaticData;

namespace Code.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;

        private IState _currentState;

        public GameStateMachine(
            IRectMeshGenerator rectMeshGenerator,
            IMeshCutter meshCutter,
            IClothFactory clothFactory,
            IClothConstraintService clothConstraintService,
            IStaticDataService staticDataService,
            IPlayerControlService playerControlService,
            IUpdateService updateService,
            ISceneLoader sceneLoader, 
            ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IState>() {
                [typeof(BootstrapState)] = new BootstrapState(staticDataService, this),
                [typeof(LoadGameState)] = new LoadGameState(
                        rectMeshGenerator,
                        meshCutter,
                        clothFactory,
                        clothConstraintService,
                        staticDataService,
                        playerControlService,
                        this,
                        sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState(playerControlService, updateService),
                [typeof(RestartGameState)] = new RestartGameState(coroutineRunner, staticDataService, this)
            };
        }

        public void Enter<TState>() where TState : IState
        {
            if(_currentState is IExitedState exitedState)
                exitedState.Exit();
            
            _currentState = _states[typeof(TState)];
            _currentState.Enter();
        }

    }
}