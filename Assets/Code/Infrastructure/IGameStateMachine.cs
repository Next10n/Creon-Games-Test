namespace Code.Infrastructure
{
    public interface IGameStateMachine
    {
        void Enter<TState>() where TState : IState;
    }
}