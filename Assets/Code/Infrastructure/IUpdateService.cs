namespace Code.Infrastructure
{
    public interface IUpdateService
    {
        void Update();
        void Register(IUpdatable updatable);
        void Unregister(IUpdatable updatable);
    }
}