using UnityEngine;

namespace Code.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}