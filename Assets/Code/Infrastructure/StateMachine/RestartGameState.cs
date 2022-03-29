using System.Collections;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.StateMachine
{
    public class RestartGameState : IState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStaticDataService _staticDataService;
        private readonly IGameStateMachine _gameStateMachine;

        public RestartGameState(ICoroutineRunner coroutineRunner, IStaticDataService staticDataService, IGameStateMachine gameStateMachine)
        {
            _coroutineRunner = coroutineRunner;
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            Restart();
        }

        private void Restart()
        {
            _coroutineRunner.StartCoroutine(Reload(_staticDataService.Data.ReplayDelay));
        }
        
        private IEnumerator Reload(float delay)
        {
            yield return new WaitForSeconds(delay);
            _gameStateMachine.Enter<LoadGameState>();
        }

    }
}