using Code.Core;
using UnityEngine;

namespace Code.Infrastructure.StateMachine
{
    public class GameLoopState : IExitedState, IUpdatable
    {
        private readonly IPlayerControlService _playerControlService;
        private readonly IUpdateService _updateService;

        public GameLoopState(IPlayerControlService playerControlService, IUpdateService updateService)
        {
            _playerControlService = playerControlService;
            _updateService = updateService;
        }

        public void Enter() =>
            _updateService.Register(this);

        public void Exit() =>
            _updateService.Unregister(this);

        public void Update() =>
            _playerControlService.Move();
    }
}