using Code.StaticData;

namespace Code.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IGameStateMachine _gameStateMachine;
        
        public BootstrapState(
            IStaticDataService staticDataService,
            IGameStateMachine gameStateMachine)
        {
            _staticDataService = staticDataService;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            LoadStaticData();
            LoadGame();
        }

        private void LoadStaticData() =>
            _staticDataService.Load();

        private void LoadGame() =>
            _gameStateMachine.Enter<LoadGameState>();
    }
}