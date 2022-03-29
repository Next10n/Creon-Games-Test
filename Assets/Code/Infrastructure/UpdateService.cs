using System.Collections.Generic;

namespace Code.Infrastructure
{
    public class UpdateService : IUpdateService
    {
        private readonly List<IUpdatable> _updatables = new List<IUpdatable>();

        public void Update()
        {
            for(int i = 0; i < _updatables.Count; i++)
                _updatables[i].Update();

            // foreach (IUpdatable updatable in _updatables)
            //     updatable.Update();
        }

        public void Register(IUpdatable updatable) =>
            _updatables.Add(updatable);

        public void Unregister(IUpdatable updatable) =>
            _updatables.Remove(updatable);
    }
}