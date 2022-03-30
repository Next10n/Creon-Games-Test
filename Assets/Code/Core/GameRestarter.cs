using Code.Infrastructure;
using Code.Infrastructure.StateMachine;
using Code.StaticData;
using UnityEngine;
using Zenject;

namespace Code.Core
{
    public class GameRestarter : MonoBehaviour
    {
        private IGameStateMachine _gameStateMachine;
        private IPlayerControlService _playerControlService;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine, IPlayerControlService playerControlService, ICoroutineRunner coroutineRunner,
            IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _playerControlService = playerControlService;
        }

        private void Awake() =>
            _playerControlService.PathPassed += Restart;

        private void OnDestroy() =>
            _playerControlService.PathPassed -= Restart;

        private void Restart()
        {
            _gameStateMachine.Enter<RestartGameState>();
        }
        
    }
}