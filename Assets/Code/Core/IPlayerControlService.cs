using System;

namespace Code.Core
{
    public interface IPlayerControlService
    {
        event Action PathPassed;
        void SpawnPlayer(BrokenLine brokenLine);
        void Move();
    }
}