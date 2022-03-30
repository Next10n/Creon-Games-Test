using Code.Core;
using Code.Inputs;

namespace Code.Infrastructure.StateMachine
{
    public class GameLoopState : IExitedState, IUpdatable
    {
        private readonly IPlayerControlService _playerControlService;
        private readonly IUpdateService _updateService;
        private readonly ISwipePlayerService _swipePlayerService;

        public GameLoopState(IPlayerControlService playerControlService, IUpdateService updateService, ISwipePlayerService swipePlayerService)
        {
            _playerControlService = playerControlService;
            _updateService = updateService;
            _swipePlayerService = swipePlayerService;
        }

        public void Enter() =>
            _updateService.Register(this);

        public void Exit() =>
            _updateService.Unregister(this);

        public void Update()
        {
            _playerControlService.Move();
            _swipePlayerService.DetectSwipe();
        }
    }
}