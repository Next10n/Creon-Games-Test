namespace Code.Infrastructure.StateMachine
{
    public interface IState
    {
        void Enter();
    }

    public interface IExitedState : IState
    {
        void Exit();
    }
}